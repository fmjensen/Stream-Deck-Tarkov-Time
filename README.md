# Stream Deck Tarkov Time
This plugin shows the current ingame Escape from Tarkov time on a button of your choise. Stream Deck Tarkov Time is calculated from Coordinated Universal Time (UTC). The shown Tarkov Time is updated every 7 seconds and on button press. 

Note that Stream Deck Tarkov Time does _not_ need the Escape from Tarkov game to be running to be able to show the two timezones on the button you choose.

https://user-images.githubusercontent.com/3484684/178591353-6ca02422-b71a-45bc-82c4-25ac22898f3c.mp4

# Requirements
* Windows 10 or higher.
* ~~.NET Core 3.1 - Get the x64 for Desktop [here](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) if you do not have it already.~~ This should not be nessesary anymore.

# Installation
1.  Download the _com.cmdrflemming.tarkovtime.streamDeckPlugin_ file from the [latest release](https://github.com/fmjensen/Stream-Deck-Tarkov-Time/releases/latest) to you computer.
2.  Doubble-click the _com.cmdrflemming.tarkovtime.streamDeckPlugin_ file.
3.  Answer **yes** to let the plugin be installed.
4.  Drag the plugin to a button on your Stream Deck.
5.  Enjoy Tarkov Time auto-update every 7 seconds and button press :)

# Trouble shooting
Open an issue on the [Issues tracker](https://github.com/fmjensen/Stream-Deck-Tarkov-Time/issues) if you believe you found a bug. Please include logs and screenshots/photos if it makes sense. I will try to help as much as possible when i got some sparetime.


# ToDo: 
Nothing is in the backlog at this time. Feel free to send suggestions and bugreports using the [Issues tracker](https://github.com/fmjensen/Stream-Deck-Tarkov-Time/issues).

## References
Here are some references for both this plugin the tools i used  and the Stream Deck:

* [Stream Deck Toolkit](https://github.com/FritzAndFriends/StreamDeckToolkit): a .NET Standard library, template, and tools for building extensions to the Elgato Stream Deck.
* Youtube video: [Building Stream Deck Plugins with C#](https://youtu.be/D5AZ_6S0f94)
* [Stream Deck Page][Stream Deck]
* [Stream Deck SDK Documentation][Stream Deck SDK]

<!-- References -->
[Stream Deck]: https://www.elgato.com/en/gaming/stream-deck "Elgato's Stream Deck landing page for the hardware, software, and SDK"
[Stream Deck software]: https://www.elgato.com/gaming/downloads "Download the Stream Deck software"
[Stream Deck SDK]: https://developer.elgato.com/documentation/stream-deck "Elgato's online SDK documentation"
[Style Guide]: https://developer.elgato.com/documentation/stream-deck/sdk/style-guide/ "The Stream Deck SDK Style Guide"
[Manifest file]: https://developer.elgato.com/documentation/stream-deck/sdk/manifest "Definition of elements in the manifest.json file"
