<template>
  <div class="login-dialog-container">
    <form class="login-dialog" @submit="onClick">
      <input required v-model="loginModel" placeholder="Логин"/>
      <input required type="password" v-model="passwordModel" placeholder="Пароль"/>
      <span class="error" v-show="hasError">{{errorMessage}}</span>
      <button>Войти</button>
    </form>
  </div>
</template>

<script setup lang="ts">

import {authModule} from "@/store/authModule";
import {computed, ComputedRef, onMounted, ref} from "vue";
import {circleModule} from "@/store/circleModule";
import {errorModule} from "@/store/errorModule";

onMounted(() => errorModule.clear());

let hasError: ComputedRef<boolean> = computed(() => errorModule.HasError);

let errorMessage: ComputedRef<string> = computed(() => {
  if (errorModule.ErrorCode && errorModule.ErrorCode == 401){
    return "Неверный логин или пароль";
  }
  else return "Неизвестная ошибка";
});

let login: string;
let loginModel = computed({
  get: () => login,
  set: (value: string) => login = value
});
let password: string;
let passwordModel = computed({
  get: () => password,
  set: (value: string) => password = value
});


async function onClick(){
  errorModule.clear();
  await authModule.login({login: login, password: password});
  if (authModule.IsAuthenticated){
    await circleModule.getCircles();
  }
}

</script>

<style scoped>
.login-dialog-container {
  z-index: 20;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: black;
}

.login-dialog {
  position: absolute;
  top: 50%;
  left: calc(50% - 150px);
  display: flex;
  align-items: center;
  justify-content: center;
  width: 300px;
  background-color: gray;
  flex-direction: column;
  padding: 10px;
  gap: 5px;
  border-radius: 5px;
}

input{
  width: 95%;
}

.error{
  align-self: start;
  color: #f69c9c;
}

</style>