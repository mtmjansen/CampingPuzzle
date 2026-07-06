import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import { startConnection } from './signalr'

startConnection().catch((error) => {
  console.error('SignalR connection failed:', error)
})

createApp(App).mount('#app')
