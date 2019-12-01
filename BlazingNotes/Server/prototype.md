# Klangbox

~~Version 3 Repositiory: Hier~~

!!! attention Attention
Wir sind schon bei Version 4 eigentlich.. muss ich noch auf schreiben um was es geht. Kurzes Stichwort: _IceCast_.
!!!

Ein Musik Deamon in Rust

## V1 und V2

War nix. Zu viel Arbeit

Warum Version 3? Es ist simpel, wir müssen NICHT decoden!!, Der HTTP Stack in Rust ist mature und in meinen Tests verhielt sich eine CURL URL fast wie ein Track was das ganze einen sehr integrierten Touch gibt.

Mal sehen wie's wird.. testen kann ich ja gut lokal...

## V3 - Der MPD Hack

Ich glaub ich hab eine Formel gefunden!

* [Overview Version 3](#overview-version-3)
* [MPD Erweiterung?](#mpd-erweiterung)

### Overview Version 3
Wir werden einfach das ganze wie Modipy auf MPD aufsetzen.

Im gegensatz zu Modipy aber werden wir nicht einfach MPD als weitere Library verwenden und am Ende einen Ball bestehend aus verschiedenen Programmen zusammengelbt mit Python abliefern sondern eine reine Rust bassierte slave Erweitertung für MPD.

Der Grund dafür ist das MPD schon fast alles kann und wir nicht Funktionalität doppeln müssen. Ausserdem ist Python ne Krücke und Klang/Tanzbox war immer für den RPI gedacht!

**Wie erreichen wir das?**

Der große Nachteil wie ich finde an MPD ist das er Plugins nur monolitisch zulässt. Es gäbe folgende Möglichkeiten:

1. Wir bauen einfach das Plugin monolitisch in MPD ein aber in Rust geschrieben.
    * Warum ich mich dagegen entschieden hab: C(++) in Rust zu hosten ist easy. Andersherum geht es bestimmt auch aber ich vermute das wird nicht so einfach gerade beim Cross-Compile. Auch müsste ich mich tief in MPD einarbeiten und at this point kann ichs auch selber machen.
2. Wir erstellen virtuelle Soundkarten und lassen MPD davon lesen.
    * Warum ich mich dagegen entschieden hab: Würde gehen aber die API ist dafür noch nicht gerostet. Ausserdem kenn ich die Nebeneffekte nicht und auch nicht wo das Maximum liegt. Und wir müssten wieder asound2 bauen für RPI.
3. Unser Plugin ist ein Audio Renderer Server und MPD klingt sich über curl einarbeite
    * Ich habe mich vorerst dafür entschieden


### MPD Erweiterung?
Was ist wenn wir uns mal MPD anschauen und alles das einbauen was uns fehlt...

~~Schritt 1 wäre MPD mal zu installieren~~

Update: MPD hat sich als leichtgewichtig, gut im verwalten und leichter Client Anbindung ausgezeichnet! Was tricky wird ist die Erweiterung aber ich hab schon eine Idee.
## Bedenken

## Bisherige Erkentnisse
1. So viel wie möglich in Rust geschrieben damit einfacher portierbar
2. Wenig bis gar keine Pipes im Hot Path (Buffering!)
3. Rodio bildet gute Basis. gstreamer wär auch ne Option gewesen aber ist schwerer zu konfigurieren
4. Fern macht sich als Logger ganz gut
5. TOML war als Config Format gut
6. Sich von Tokio so weit wie es geht fernhalten

# WIP
* Zum Playlist parsen möchte ich Nom testen oder Pest
* Der Web Loader war bisher curl, pcre2 und url ich möchte aber curl mit reqwest und prce2 mit fancy-regex weil weniger Rust


## Bevorstehendes
Alles steht noch nicht bereit.

* Ich möchte Kkompatibilität mit vielen Systemen haben deshalb hab ich an ZeroMQ gedacht oder nanomq. Evtl müssen man folgendes Portieren
    * [nng](https://nanomsg.github.io/nng/man/v1.0.0/nng.7.html) mit bindgen und cc als Rust Library einsetzen statt eigener Message Lösung

Nach dem das Projekt schon 10 Monate läuft und ich immer mehr Projekte finde die meins ersetzen glaube ich das es langsam keinen Sinn mehr macht dieses Projekt zu verfolgen.

Neben den kommerziellen Varianten gibt es

* Modipy
* MDP

Die beide irgendwie genau das machen was ich in Rust versucht habe.

Modipy is ne Krücke aber evtl kann ich MDP einfach erweitern!

### m3u8 Parsing mit nom?

Es wäre cool wenn ich mehr verstehen würde über Text Parsing. m3u8 hab ich ja schon ganz erfolgreich ohne Tools geparst. Also denke ich das es eine gute Übung wäre den m3u8 Parser mit nom noch mal zu bauen

https://github.com/Geal/nom
https://docs.rs/nom/4.0.0/nom/
https://stevedonovan.github.io/rust-gentle-intro/nom-intro.html

_Kann aber auch sein dass das der MPD für uns macht._



## Beispiel Cargo Config

```toml
rodio = "0.8"
log = "0.4"
fern = "0.5"
ctrlc = { version = "3.1", features = ["termination"] }
nom = "4.0"
fancy-regex = { git = "https://github.com/fancy-regex/fancy-regex.git" }
url = "1.7"
reqwest = "0.9"
serde = "1.0"
serde_derive = "1.0"
toml = "0.4"
```


## Update - 18.11.18

Lang geschlafen. Mag den Music Player Deamon also hab ich mich mal informiert wie meine Erweiterung ausschauen könnte. Man müsste ja quasi eine MP3 oder so streamen. Also HTTP 1.1 und Chunked Body denke ich. Das ist aber ne Herrausvorderung wenn man sich vom Tokio Zeug fern halten will obwohl der Webserver ja eigentlich nur für RPC Zwecke dient.

Ich hab mir jetzt ein paar Möglichkeiten angeschaut und denke das es mit [nickel.rs](http://nickel-org.github.io/) am einfachsten geht. Als Prototyp sollte ich nicht direkt librespot einbauen sondern etwas ganz einfaches. Vieleicht einfach FS Streaming dann kann ich mich ganz auf das Chunked Streaming konzentrieren.

## Update - 21.11.18

Version 4?? Wenns mehr Hardware Support geben soll braucht es das. Aber dann brauch ich auch nen Metall Bohrer. Wäre auch im Code eleganter und viele Features wären einfacher zu handeln. Aber Audio playing is hard. REALLY HARD.

Rodio hat eigentlich gepasst. Protkollmäßig kann man es ja so ähnlich wie MPD machen das war ja ganz easy zu verstehen.

Ist JACK eigentlich so viel schwerer?

## Update: 02.12.18


Ich hatte ja schon länger die Idee wie Alpine mehr vom System im RAM laufen zu lassen aber nicht alles und vorallem mit Raspbian Support. In meiner Unwissenheit dachte ich ich müsste dafür nen neuen Kernel bauen aber alles was man braucht ist schon im Kernel drin. Siehe Links unten

- [Explaining OverlayFS – What it Does and How it Works](https://www.datalight.com/blog/2016/01/27/explaining-overlayfs-%E2%80%93-what-it-does-and-how-it-works/)
- [The Overlay Filesystem](https://windsock.io/the-overlay-filesystem/)
- [tmpfs - ArchWiki](https://wiki.archlinux.org/index.php/Tmpfs)
- [Raspberry Pi, overlayfs read-write root, read-only NFS base](https://blockdev.io/read-only-rpi/)
- [Setting up overlayFS on Raspberry Pi](https://www.domoticz.com/wiki/Setting_up_overlayFS_on_Raspberry_Pi)
- [How do I use OverlayFS? - Ask Ubuntu](https://askubuntu.com/questions/109413/how-do-i-use-overlayfs)

Ausserdem war mein [F2FS Experiment](https://movr0.com/2016/08/19/convert-raspberry-pi-123-to-f2fs/) erfolgreich. Ist jedoch etwas umständlich... ich frage mich ob man das automatisieren oder remixen kann.

Dann ist noch die Frage wie langsam die SSD ist. Muss ich noch [Benchmarken](<https://askubuntu.com/questions/87035/how-to-check-hard-disk-performance>). Falls die SD Karte schneller sein sollte könnte man das ganze noch mit [dm-writeboost](https://github.com/akiradeveloper/dm-writeboost) verknüpfen. Dann hat man am Ende ein haufen Filesystem Tech zusammengehackt aber das müsste optimal laufen.

#### Visualisierung

[Visualizations with Web Audio API - Web APIs \| MDN](https://developer.mozilla.org/en-US/docs/Web/API/Web_Audio_API/Visualizations_with_Web_Audio_API)

Mir fehlt ja bisher nur das Konzept da ists egal ob Web Audio oder echtes Audio.

#### Windows Audio Streaming

Vieleicht findet sich hier was? https://github.com/acidhax/chromecast-audio-stream

#### Google Play Music

[Google-Play-Music-Desktop-Player-UNOFFICIAL: A beautiful cross platform Desktop Player for Google Play Music](https://github.com/MarshallOfSound/Google-Play-Music-Desktop-Player-UNOFFICIAL-)
## Latest
**Update: 05.12.18**

UI Lösung gefunden?: [Flutter on Raspberry Pi (mostly) from scratch](https://medium.com/flutter-io/flutter-on-raspberry-pi-mostly-from-scratch-2824c5e7dcb1)