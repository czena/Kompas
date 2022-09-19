<template>
  <div class="circle-edit-dialog-container">
    <div class="circle-edit-dialog">
      <span>{{ props.id }}</span>
      <input v-model="value"/>
      <button @click="onClick">"сохранить изменения"</button>
    </div>
  </div>
</template>

<script setup lang="ts">

import {computed, ref, Ref} from "vue";

interface ICircleEdit {
  id: number;
  description: string;
}

const props = defineProps<ICircleEdit>();
const emit = defineEmits<{ (e: 'update:description', val: string): void }>();

let input: Ref<string> = ref(props.description);
let value: Ref<string> = computed({
  get: () => input.value,
  set: (value: string) => input.value = value
});

function onClick() {
  emit('update:description', input.value);
}

</script>

<style scoped>

.circle-edit-dialog-container {
  z-index: 10;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(256, 256, 256, .50);
}

.circle-edit-dialog {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  display: -ms-flexbox;
  display: flex;
  -ms-flex-align: center;
  align-items: center;
  -ms-flex-pack: center;
  justify-content: center;
  width: 500px;
  background-color: gray;
  flex-direction: column;
}
</style>