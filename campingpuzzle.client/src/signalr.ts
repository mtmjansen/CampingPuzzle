import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

let connection: HubConnection | null = null

export async function startConnection() {
  connection = new HubConnectionBuilder()
    .withUrl('/campingHub')
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build()

  connection.on('TileTypeRead', (payload: { row: number; column: number; type: string }) => {
    console.log('Tile.Type getter read:', payload)
    const event = new CustomEvent('tile-type-read', { detail: payload })
    window.dispatchEvent(event)
  })

  await connection.start()
  console.log('SignalR connected to /campingHub')
}
