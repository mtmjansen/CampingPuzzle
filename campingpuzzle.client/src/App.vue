<script setup lang="ts">
import { onMounted, ref } from 'vue'
import Camping from './components/Camping.vue'
import './signalr'

const lastTileEvent = ref<{ row: number; column: number; type: string } | null>(null)

async function callTileType() {
  try {
    const response = await fetch('/Puzzle/?row=4&column=6')
    const text = await response.text()
    console.log('PuzzleController.GetTileType response:', text)
  } catch (error) {
    console.error('Error calling GetTileType:', error)
  }
}

onMounted(() => {
  window.addEventListener('tile-type-read', (event: Event) => {
    const customEvent = event as CustomEvent<{ row: number; column: number; type: string }>
    lastTileEvent.value = customEvent.detail
  })
})
</script>

<template>
  <header>
    <img id="vue-logo" alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" @click="callTileType" />
  </header>

  <main>
      <Camping :rows="10" :cols="10" />
      <section class="signalr-events" v-if="lastTileEvent">
        <h2>Last Tile.Type read</h2>
        <p>Row: {{ lastTileEvent.row }}, Col: {{ lastTileEvent.column }}, Type: {{ lastTileEvent.type }}</p>
      </section>
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
