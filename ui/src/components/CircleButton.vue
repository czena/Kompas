<template>
  <div class="circle-button-container" @click="onClick" />
</template>

<script setup lang="ts">

import {Circle} from "@/models/Circle";

interface ICircleButton {
  circle: Circle;
}

const props = defineProps<ICircleButton>();

const emit = defineEmits<{
  (e: 'edit', circle: Circle): void,
}>();

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
  top: calc(v-bind(props.circle.y + 'px') - 15px);
  left: calc(v-bind(props.circle.x + 'px') - 15px);
  background-color: red;
  border: 1px solid black;
}
</style>