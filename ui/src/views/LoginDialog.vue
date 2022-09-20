<template>
  <div class="login-dialog-container">
    <div class="login-dialog">
      <input required v-model="loginModel" placeholder="Логин"/>
      <input required type="password" v-model="passwordModel" placeholder="Пароль"/>
      <button @click="onClick">"Войти"</button>
    </div>
  </div>
</template>

<script setup lang="ts">

import {authModule} from "@/store/authModule";
import {computed} from "vue";
import {circleModule} from "@/store/circleModule";

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


function onClick(){
  console.log(login, password);
  authModule.login({login: login, password: password});
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