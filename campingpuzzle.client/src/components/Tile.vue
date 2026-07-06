<template>
  <button class="tile" :class="{ 'tile--active': active }" @click="$emit('click')" type="button">
    <span class="tile__content">
      <span v-if="value !== undefined">{{ value }}</span>
      <span v-else>{{ row }},{{ col }}</span>
    </span>
  </button>
</template>

<script setup lang="ts">
const props = defineProps({
  row: { type: Number, required: true },
  col: { type: Number, required: true },
  value: { type: [String, Number], default: undefined },
  active: { type: Boolean, default: false }
})

const emit = defineEmits<{
  (event: 'click'): void
}>()
</script>

<style scoped>
.tile {
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

.tile__content {
  font-weight: 600;
  line-height: 1;
}
</style>
