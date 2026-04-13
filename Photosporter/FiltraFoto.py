import os
import shutil
import numpy as np
from PIL import Image
import imagehash 
from sklearn.cluster import DBSCAN

def pulisci_foto(cartella,cartella_destinazione,soglia=15):
    file_immagini= [   
        f  for f in os.listdir(cartella)
        if f.lower().endswith(('png', 'jpg', 'jpeg'))
    ]
    #crea la cartella di destinazione se non esiste
    path_puliti=os.path.join(cartella_destinazione)
    if not os.path.exists(path_puliti):
        os.makedirs(path_puliti)
    # carica dentro file_immagini tutti i contenuti dalla cartella che finiscono con un formato  foto
    hash_list, nomi_validi=[],[]
    for nome in file_immagini:
        try:
            with Image.open(os.path.join(cartella,nome)) as img:
                #calcola l'hash e lo appiettisce in una riga  di numeri (0 e 1)
                h=imagehash.phash(img)
                hash_list.append(h.hash.flatten())
                nomi_validi.append(nome)
        except:
            continue #se il file è corrotto lo salta

    if not hash_list:
        print("Nessuna immagine valida trovata.")
        return
    X= np.array(hash_list) #creo un matrice di ash list
    clustering=DBSCAN(eps=soglia,min_samples=2,metric='manhattan').fit(X)
    #esp soglia definisce quanto una foto devono essere "simili" per essere considerati la stessa
    #metric indica il tipo di "unita di misura del algoritmo"
    #fit(X) in base alle informazioni date nel dbscan etichetta le varie immagini aggiungendo un etichetta
    foto_da_copiare=[]
    Duplicati={}
    for i, label in enumerate(clustering.labels_):
        nome_file=nomi_validi[i]
        if label==-1: #ignora le foto uniche (etichetta con -1)
            foto_da_copiare.append(nome_file)
        else:
            if label not in Duplicati:
                Duplicati[label]=[]
                Duplicati[label].append(nome_file)

    #controllo delle foto migliore per ogni gurppo di foto simili
    for id_gruppo, lista_foto in Duplicati.items():
        #scegliamo la foto piu pesante che nel caso del formato jpg è quella con la migliore qualità
        foto_migliore=max(lista_foto,key= lambda f: os.path.getsize(os.path.join(cartella,f)))
        foto_da_copiare.append(foto_migliore)
        
    #copio fisicia delle foto nella cartella di destinazione
    print(f"Copiando {len(foto_da_copiare)} foto nella cartella {path_puliti}...")
    for foto in foto_da_copiare:
        shutil.copy(os.path.join(cartella,foto),os.path.join(path_puliti,foto))
    print("Operazione completata.")

if __name__=="__main__":
   
   cartella_input=input("Inserisci il percorso della cartella con le foto da filtrare: ")
   cartella_output=input("Inserisci il percorso della cartella dove salvare le foto filtrate: ")
   soglia=int(input("Inserisci la soglia di similarità (es. 5): "))
   pulisci_foto(cartella_input,cartella_output,soglia)

        
