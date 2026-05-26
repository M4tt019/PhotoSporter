import argparse
import os
import shutil
import sys

import imagehash
import numpy as np
from PIL import Image
from sklearn.cluster import DBSCAN

#scelta estensioni immagini
ESTENSIONI_IMMAGINI = (".png", ".jpg", ".jpeg")

#metodo che prende tutti le foto della cartella, solo questi restitutendolo una lista di root
def trova_immagini(cartella):
    immagini = []
    for root, _, files in os.walk(cartella):
        for nome in files:
            if nome.lower().endswith(ESTENSIONI_IMMAGINI):
                immagini.append(os.path.join(root, nome))
    return immagini

#metodo principale dello scrypt
def pulisci_foto(cartella, cartella_destinazione=None, soglia=15, cancella=False):
    file_immagini = trova_immagini(cartella)
    hash_list, nomi_validi = [], []
    #ciclo le immagini della lista di root
    for percorso in file_immagini:
        try:
            with Image.open(percorso) as img: #se il percorso è un immagine (tipo) 
                h = imagehash.phash(img) #creo l'ash del immagine, ridinesiona in 32x32, la converte in scala di grigi, apllica il DCT,
                                        #estrate i valori a bassa frequenze, e ne calcola la media del valore (averege pooling)
                hash_list.append(h.hash.flatten()) #lo inserisco nella lista di hash 
                nomi_validi.append(percorso)
        except Exception as ex:
            print(f"Immagine saltata: {percorso} ({ex})")
#controllo
    if not hash_list:
        print("Nessuna immagine valida trovata.")
        return 0

    x = np.array(hash_list) #converte la lista in valori di hash
    clustering = DBSCAN(eps=soglia, min_samples=2, metric="manhattan").fit(x) #vengono raggruppati gli elementi simili (vicini) usando una soglia

    foto_da_tenere = set()   # insieme di foto da conservare (no duplicati grazie al set)
duplicati = {}           # dizionario: label → [lista di foto in quel cluster]
    for i, label in enumerate(clustering.labels_):
        nome_file = nomi_validi[i] #è un immagine duplicata, viene salvato in un gruppo nel dizionario duplicati
        if label == -1: #signofica che è un immagine "unica" quindi la salva
            foto_da_tenere.add(nome_file)
        else:
            duplicati.setdefault(label, []).append(nome_file)
  #fra i duplicati viene scelta la immagina con maggiore qualità, migliorando inserendo un filtro ML per la selezione delle immagini multiple
    for lista_foto in duplicati.values():
        foto_migliore = max(lista_foto, key=lambda f: os.path.getsize(f))
        foto_da_tenere.add(foto_migliore)

foto_da_rimuovere = []
for foto in nomi_validi:
    if foto not in foto_da_tenere:
        foto_da_rimuovere.append(foto)
        
        #mostra in output quante foto sono stte cancellate, in modo da dare una prospettiva al utente
    if cancella:
        for foto in foto_da_rimuovere:
            os.remove(foto)
        print(f"Doppioni cancellati: {len(foto_da_rimuovere)}")
        print(f"Foto mantenute: {len(foto_da_tenere)}")
        return len(foto_da_rimuovere)

    if not cartella_destinazione:
        raise ValueError("La cartella di destinazione e' obbligatoria quando cancella=False.")

    os.makedirs(cartella_destinazione, exist_ok=True)
    for foto in foto_da_tenere:
        percorso_relativo = os.path.relpath(foto, cartella)
        destinazione = os.path.join(cartella_destinazione, percorso_relativo)
        os.makedirs(os.path.dirname(destinazione), exist_ok=True)
        shutil.copy2(foto, destinazione)

    print(f"Foto copiate nella cartella {cartella_destinazione}: {len(foto_da_tenere)}")
    return len(foto_da_rimuovere)


def leggi_argomenti():
    parser = argparse.ArgumentParser(description="Filtra o cancella foto duplicate.")
    parser.add_argument("--input", help="Cartella da analizzare.")
    parser.add_argument("--output", help="Cartella in cui copiare le foto filtrate.")
    parser.add_argument("--soglia", type=int, default=15, help="Soglia di similarita'.")
    parser.add_argument("--cancella", action="store_true", help="Cancella i doppioni sul posto.")
    return parser.parse_args()


if __name__ == "__main__":
    args = leggi_argomenti()

    cartella_input = args.input or input("Inserisci il percorso della cartella con le foto da filtrare: ")
    cartella_output = args.output
    if not args.cancella and not cartella_output:
        cartella_output = input("Inserisci il percorso della cartella dove salvare le foto filtrate: ")

    try:
        pulisci_foto(cartella_input, cartella_output, args.soglia, args.cancella)
    except Exception as errore:
        print(f"Errore: {errore}", file=sys.stderr)
        sys.exit(1)
