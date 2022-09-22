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
import {GetDescriptionRequest} from "@/contract/requests/getDescriptionRequest";
import axios, {AxiosError} from "axios";
import env from "../../environment";
import {GetDescriptionResponse} from "@/contract/responses/getDescriptionResponse";
import {circleModule} from "@/store/circleModule";
import {errorModule} from "@/store/errorModule";

let hasError: ComputedRef<boolean> = computed(() => errorModule.HasError);

let errorMessage: ComputedRef<string> = computed(() => {
  return "Неизвестная ошибка";
});

interface ICircleEdit {
  id: number;
}

onMounted(async() => {
  await getDescription();
})

const props = defineProps<ICircleEdit>();
const emit = defineEmits<{ (e: 'close', val: string): void }>();

async function getDescription(){
  let request = new GetDescriptionRequest(props.id);
  await axios.post(env.GetDescriptionApi, request, {
    headers:{
      Authorization: "Bearer " + localStorage.getItem("token")
    }
  }).then(function(res){
    if (res.status == 200){
      input.value = (<GetDescriptionResponse>res.data).description;
    }
    else errorModule.setCode(res.status)
  }).catch(function(error: AxiosError){
    errorModule.setError(error);
  });
}

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
  if (!hasError.value)  emit('close', input.value);
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