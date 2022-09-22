<template>
  <div class="circle-edit-dialog-container">
    <div class="circle-edit-dialog">
      <div class="head">
        <span class="id">{{ props.id }}</span>
        <span class="close" @click="onClose">X</span>
      </div>
      <input v-model="value"/>
      <span class="error" v-show="hasError">{{errorMessage}}</span>
      <button @click="onClick">сохранить изменения</button>
    </div>
  </div>
</template>

<script setup lang="ts">

import {computed, ComputedRef, onMounted, ref, Ref} from "vue";
import {circleModule} from "@/store/circleModule";
import {errorModule} from "@/store/errorModule";

interface ICircleEdit {
  id: number;
}

const props = defineProps<ICircleEdit>();
const emit = defineEmits<{ (e: 'close', val: string): void }>();

let hasError: ComputedRef<boolean> = computed(() => errorModule.HasError);

let errorMessage: ComputedRef<string> = computed(() => {
  return "Неизвестная ошибка";
});

onMounted(async() => {
  input.value = await circleModule.getDescription(props.id) ?? "";
});

let input: Ref<string> = ref("");
let value: Ref<string> = computed({
  get: () => input.value,
  set: (value: string) => input.value = value
});

function onClose(){
  emit('close', input.value);
}

async function onClick() {
  await circleModule.setDescription({id: props.id, newDescription: input.value});
  if (!hasError.value) emit('close', input.value);
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
  left: calc(50% - 150px);
  display: flex;
  align-items: center;
  justify-content: center;
  width: 300px;
  background-color: gray;
  flex-direction: column;
  gap: 10px;
  padding-bottom: 10px;
}

.head{
  display: flex;
  flex-direction: row;
  width: 100%;
  background-color: black;
  justify-content: space-between;
}

input{
  width: 90%;
}

.head span{
  color: white;
  padding: 5px;
}

.close:hover{
  color: red;
}

.error{
  align-self: start;
  color: #f69c9c;
  padding-left: 5px;
  padding-right: 5px;
}

</style>