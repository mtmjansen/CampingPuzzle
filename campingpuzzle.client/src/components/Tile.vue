<script setup lang="ts">
import { onUnmounted, ref } from 'vue';

const props = defineProps({
  row: { type: Number, required: true },
  col: { type: Number, required: true },
  value: { type: [String, Number], default: undefined },
  active: { type: Boolean, default: false }
})

const b = ref(0)
let flashTimeout: ReturnType<typeof setTimeout> | undefined

function flash() {
  if (flashTimeout !== undefined) {
    clearTimeout(flashTimeout)
  }

  b.value = 2.5
  flashTimeout = setTimeout(() => {
    b.value = 0
    flashTimeout = undefined
  }, 300)
}

onUnmounted(() => {
  if (flashTimeout !== undefined) {
    clearTimeout(flashTimeout)
  }
})

defineExpose({ flash })

const emit = defineEmits<{
  (event: 'click'): void
}>()
</script>

<template>
  <button
    class="tile"
    :class="{ 'tile--active': active }"
    :style="{ '--flash-border-width': `${b}px` }"
    @click="$emit('click')"
    type="button"
  >
    <span class="tile__content">
      <span v-if="value !== undefined">{{ value }}</span>
      <span v-else>{{ row }},{{ col }}</span>
    </span>
  </button>
</template>

<style scoped>

.tile {
  position: relative;
  width: 100%;
  aspect-ratio: 1;
  border: 1px solid var(--tile-border, #d1d5db);
  border-radius: 0.5rem;
  background: var(--tile-background, #f8fafc);
  color: var(--tile-foreground, #111827);
  display: grid;
  place-items: center;
  cursor: pointer;
  transition: background-color .2s ease, transform .2s ease, border-color .2s ease;
  padding: 0;
}

.tile:hover {
  background-color: var(--tile-hover, #e2e8f0);
}

.tile--active {
  background-color: var(--tile-active, #60a5fa);
  color: white;
  border-color: rgba(96, 165, 250, .75);
  transform: translateY(-1px);
}

.tile::after {
  position: absolute;
  inset: 0;
  border: var(--flash-border-width) solid red;
  border-radius: inherit;
  content: '';
  pointer-events: none;
  transition: border-width 0.3s ease;
}

.tile__content {
  font-weight: 600;
  line-height: 1;
}
</style>
