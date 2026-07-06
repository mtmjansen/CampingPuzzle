<template>
  <button class="tile" :class="{ 'tile--active': active }" @click="$emit('click')" type="button">
    <span class="tile__content">
      <!-- <span v-if="value !== undefined">{{ value }}</span>
      <span v-else>{{ row }},{{ col }}</span> -->
        <img alt="tree" class="icon" src="../assets/tree.svg" width="100%" height="100%" />
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
  /* width: 10%;
  height: 40%; */
  aspect-ratio: 1;
  border: 0.2rem solid var(--tile-border, #d1d5db);
  /* border: 1px solid var(--tile-border, #d1d5db); */
  border-radius: 0.25rem;
  background: var(--tile-background, #f8fafc);
  color: var(--tile-foreground, #5d77af);
  display: grid;
  place-items: left;
  cursor: pointer;
  transition: background-color .2s ease, transform .2s ease, border-color .2s ease;
  /* padding: 1rem; */
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
