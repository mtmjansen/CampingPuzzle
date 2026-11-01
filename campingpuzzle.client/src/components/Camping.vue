<script setup lang="ts">
import { onMounted, onUnmounted, computed, reactive } from 'vue'
import Tile from './Tile.vue'

const props = defineProps({
  rows: { type: Number, required: true },
  cols: { type: Number, required: true },
  initialActive: { type: Array as () => Array<[number, number]>, default: () => [] }
})

const state = reactive({
  tiles: Array.from({ length: props.rows * props.cols }, (_, index) => {
    const row = Math.floor(index / props.cols)
    const col = index % props.cols
    const active = props.initialActive.some(([r, c]) => r === row && c === col)

    return { row, col, value: 'empty' as string | number, active }
  })
})

const tiles = computed(() => state.tiles)
const tileRefs = new Map<string, { flash: () => void }>()

const gridStyle = computed(() => ({
  gridTemplateColumns: `repeat(${props.cols}, minmax(0, 1fr))`,
  gridTemplateRows: `repeat(${props.rows}, minmax(0, 1fr))`
}))

function setTileRef(key: string, instance: unknown) {
  if (
    typeof instance === 'object' &&
    instance !== null &&
    'flash' in instance &&
    typeof instance.flash === 'function'
  ) {
    tileRefs.set(key, instance as { flash: () => void })
  } else {
    tileRefs.delete(key)
  }
}

function flashTile(row: number, col: number) {
  const key = `${row}-${col}`
  tileRefs.get(key)?.flash()
}

function toggleTile(row: number, col: number) {
  const tile = state.tiles.find((tile) => tile.row === row && tile.col === col)
  if (tile) {
    tile.active = !tile.active
  }
}

function handleTileTypeRead(event: Event) {
  const customEvent = event as CustomEvent<{ row: number; column: number; type: string }>
  flashTile(customEvent.detail.row, customEvent.detail.column)
}

onMounted(() => window.addEventListener('tile-type-read', handleTileTypeRead))
onUnmounted(() => window.removeEventListener('tile-type-read', handleTileTypeRead))
</script>

<template>
  <div class="camping">
    <div class="camping__grid" :style="gridStyle">
      <Tile
        v-for="tile in tiles"
        :key="`${tile.row}-${tile.col}`"
        :row="tile.row"
        :col="tile.col"
        :value="tile.value"
        :active="tile.active"
        :ref="(instance) => setTileRef(`${tile.row}-${tile.col}`, instance)"
        @click="toggleTile(tile.row, tile.col)"
      />
    </div>
  </div>
</template>

<style scoped>
.camping {
  width: 100%;
  padding: .25rem;
  background-color: var(--camping-background, #a8aaac);
}

.camping__grid {
  display: grid;
  gap: 0.1rem;
}
</style>
