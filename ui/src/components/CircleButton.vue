<template>
  <div class="circle-button-container" @click="onClick" />
</template>

<script setup lang="ts">

import {Circle} from "@/models/circle";
import {computed, ComputedRef} from "vue";
import {circleModule} from "@/store/circleModule";

interface ICircleButton {
  circle: Circle;
}

const props = defineProps<ICircleButton>();

const emit = defineEmits<{
  (e: 'edit', id: Circle): void,
}>();

let minPosition: ComputedRef<{x: number, y: number}> = computed(() => circleModule.CirclesMinPosition);

function onClick(){
  emit('edit', props.circle);
}

</script>

<style scoped>
.circle-button-container{
  position: absolute;
  display: flex;
  border-radius: 15px;
  width: 30px;
  height: 30px;
  top: calc(v-bind(props.circle.y + 'px') - 15px - v-bind(minPosition.y + 'px'));
  left: calc(v-bind(props.circle.x + 'px') - 15px - v-bind(minPosition.x + 'px'));
  background-color: red;
  border: 1px solid black;
}
</style>