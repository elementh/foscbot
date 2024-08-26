# FOSCBot

[FOSC](https://fosc.space!)'s official telegram bot. Find it here: [@foscbot](https://t.me/foscbot).

It's full of internal memes, jokes and stuff. Use it inline or just add it to a grup for some geek fun. It is also `c̵̉u̸̚r̴̄s̵̎e̷̋d̴͂`: it can understand python scripts.

Made with ❤️ and gratitude by [Lucas Maximiliano Marino](https://lucasmarino.me) and [LinuxCT](https://github.com/linuxct).

This bot is powered by [Navigator](https://github.com/navigatorframework/navigator).

## Usage

There are many commands available to the users, check them out by starting a conversation with [@foscbot](https://t.me/foscbot).

Leaving commands aside there are a lot of jokes and triggers which will launch themselves when necessary and are guaranteed to make all the group laugh a bit.

## Host it yourself

You can find the container images [here](https://github.com/users/elementh/packages/container/package/foscbot).

You will need to setup some ENV variables:

- `DB_CONNECTION_STRING`. The connection string to your postgresql database.
- `TELEGRAM_TOKEN`. Your telegram bot token.
- `BOT_URL`. This bot runs on webhooks, you need to specify the domain which your bot will receive updates.
- `GIPHY_API_KEY`. Your giphy api key for the gif responses.

## License

FOSCBot - FOSC's official telegram bot.

Copyright (C) 2022  Lucas Maximiliano Marino <https://lucasmarino.me>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.
