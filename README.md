# ExordiumInventoryTask
Inventory System developed in Unity3D as a part of Exordium Programming Assignment

!!!! NAPOMENA: 
______________
Ukoliko se igra testira pokretanjem build verzije, a ne unutar editora, funkcionalnosti Equippement Panela izostaju iz nepoznatog razloga. 
Gumb za Unequip i RMB za istu akciju ne ažuriraju prikaz slotova unutar Equip Panela ali se isti itemi pojavljuju unutar slotova Inventory Panela
i statistika se računa kao da je Unequip akcija izvršena. Ako je igra pokrenuta unutar editora, sve funkcionalnosti svih panela rade ispravno i slotovi se ažuriraju ispravno.
Zbog vremenskog ograničenja nisam uspio otkriti uzrok takve specifične razlike unutar editora i builda.

Skaliranje sa veličinom ekrana je podešeno za 16:9 i 16:10, no moguće su sitne razlike unutar editora za razliku od build verzije. Unutar editora testiranje je preporučeno
u FullHD(1920x1080).

UPUTE:
______
W,A,S,D - kretanje igrača
Space - spawning random itema na random lokacijama
I - otvaranje Inventory Panela
X - otvaranje Equippement Panela
Y - otvaranje Character Stats Panela
E ili Left Mouse Button na item ako je u dosegu - skupljanje itema 

INVENTORY PANEL:
________________
Item select + Backspace - odbacivanje čitavog stacka itema
Pritisak crvenog gumba iznad itema - odbacivanje samo jedne instance itema (u slučaju da ima više od jednog stacka)
Right Mouse Button unutar na item koji je equippable - equippanje itema
Middle Mouse Button unutar na item koji je consumeable - consumeanje itema
Drag itema izvan granica Inventory Panela - odbacivanje čitavog stacka itema

EQUIPPEMENT PANEL:
__________________
Pritisak crvenog gumba ili Right Mouse Button iznad itema - povratak itema u inventory

ISPUNJAVANJE TASKOVA:
_____________________
1. Svi taskovi vezani za kretanje su ispunjeni, korišteno je 2D physics-based kretanje, sprite sheet koji je predložen na dnu dokumenta,
kamera prati igrača, animacija je prilagođena brzini kretanja i svijet sadrži 3 elementa koji su collidable sa igračem (a da nisu itemi).

2. Igrač može sakupljati iteme pritiskom tipke 'E' ili lijevo klika miša ukoliko se nalazi u dometu trigger collidera itema 
koji je praćen pomoću physics overlap circle-a. Igrač može koristiti i jednu i drugu metodu.

3. Gumbi za otvaranje i zatvaranje panela za Inventory, Equippement ili Stats nalaze se u središnjem donjem dijelu ekrana
i označeni su sa I,E,S. Gumbi za zatvaranje panela nalaze se na svakom pojedinom panelu. Shortcuti za otvaranje panela su tipke 'I', 'X', 'Y'.

4. Inventory Grid Screen defaultno ima 7 slotova (koji čine jedan red). Prilikom dodavanja 8. itema, stvara se novi red slotova.
Ukoliko je detektiran prazan red, u logovima se pojavljuje poruka da je omogućeno brisanje praznog reda no brisanje nije implementirano zbog nedostatka vremena za debuggiranje.

5. Equip Screen sadrži slotove Head, Body, Wapon, Shield koji su fiksirani

6. Ako je sakupljen item koji je tipa equippable i slot za vrstu equippable itema je prazan, item je smješten u pripadajući slot unutar Equip Screena.
U protivnom item je smješten u slot unutar Inventory Screena.

7. Unutar Inventory Screena moguće je mijenjati mjesta itemima i obavljati zamjenu mjesta dvaju itema. Navedeno ne vrijedi i za Equippement screen zato što je 
napravljeno na način da Equip Screen i Inventory Screen ne mogu biti otvoreni istovremeno. Slotovi unutar Equip Screena nemaju drag n drop funkcionalnost.
Ako je Inventory Screen zatvoren dok je item odabran ili "nošen", izvršava se Deselect itema i vraćen je na prethodnu poziciju unutar slota Inventory Screena.
Ako je item nošen i pušten izvan granica Inventory Panela, više se ne nalazi u Inventory Screenu nego se stvara na poziciji igrača na zemlji.
Desni klik izvršen iznad Equipable itema izvršava Equip na odgovarajući slot unutar Equip Screena, a ako se tamo već nalazi item u tom slotu,
stari item se vrača u Inventory i biva zamijenjen novim. Ako je item selectan unutar Inventory Screena (označen je njegov border) i pritisnuta je tipka Backspace,
čitav stack itema je spušten na zemlju pored igrača. Pritisak kotačića miša iznad consumeable itema izvršava akciju consumeanja.
Ukoliko se igrač nalazi u dometu itema prije sakupljanja, slika itema je uvećana kako bi se istaknula da je moguće izvršiti akciju nad tim itemom (umjesto highlight) .

8. Postoje itemi koji su Permanent Usage i ne spremaju se u inventory nego odmah mijenjaju statistiku igrača. Equipable items se pohranjuju u inventory i equip slotove,
consumeable items se pohranjuju u inventory i mogu se koristiti pritiskom kotačića miša. Equipable i Consumeable items mogu biti Stackable sa ograničenim ili neograničenim brojem stackova.
Prilikom pomicanja itema unutar Inventory Screena, pomiče se samo slika itema sa smanjenom alfa komponentom te se zbog toga ne pomiče i stack display.
Takav način pomicanja elementa napravljen je namjerno iz estetskih razloga.
Pritiskom tipke Space random item prefabovi se spawnaju na random pozicijama mape. 

Itemi se nalaze unutar Data Struct foldera i imaju sljedeće postavke:

*Armor - equipabable type Body, nije stackable, agility i defense modifier
*Hammer - equipable type Weapon, nije stackable, agility i attack modifier
*Helmet - equipable type Head, nije stackable, agility i defense modifier
*Shield - equipable type Shield, nije stackable, agility i defense modifier
*Sword - equipable type Weapon nije stackable, agility i attack modifier
*Potion - stackable, consumeable, stack limit 5, health modifier
*Berries - stackable, consumeable, unlimited, health modifier
*Cherry - permanent usage, health modifier

9.
Itemi imaju pridružene StatModifier komponente koje mijenjaju statistiku igrača. Statistika je predočena unutar Stats Panela.
Ukoliko je consumeanjem ili equipanjem nekog itema prekoračena gornja ili donja granica nekog atributa (manje od 0 ili veće od 100)
akcija se ne izvršava i odgovarajuća poruka se prikazuje unutar logova.

10. 
Kreiran je privatni repozitory na GitHubu: https://github.com/arihus11/ExordiumInventoryTask 
Production branch sadrži završnu verziju za testiranje. 
Linka na Google Drive koji sadrži datoteke: https://drive.google.com/drive/folders/16sKZ_NZs67an1ZztrC3J7RiVC09Cl8tJ?usp=sharing

SKRIPTE:
________
- CollisionDetection, FollowPlayer (UI i kamera slijede igrača), PickUpMechanics, PlayerMovement - kretanje igrača i kolizija
- Item, ItemButton, ItemSpawner - definiraju item općenito, gumb za uklanjanje itema iz inventory screena, spawn itema pritiskom tipke Space
- CharacterStatModifierSO, ConsumeableItemSO, EquippableItemSO, EquippementSO, InventorySO, ItemSO, PermanentUsageItemSO, StatSO - scriptable objecti koji definiraju istoimene objekte sa njihovim atributima i akcijama koje je nad njima moguće izvoditi
- StatController, StatUI - skripte za ažuriranje statistike
- InventoryController, InventoryItem, InventorySlots - skripte za kontroliranje operacija na itemima, ili slotovima inventory screena
- AgentWeapon (međusobna zamjena equipable itema npr. čekić za mač i obratno), EquipController, EquippementItem,EquippementSlots - kontrola akcija nad slotovima Equipment screena i nad itemima koji su trenutno equipped
- CharacterStatAttackModifier, CharacterStatHealthModifier, CharacterStatAgilityModifier, CharacterStatDefenseModifier - definiraju akcije koje mijenjaju vrijednosti statistike
