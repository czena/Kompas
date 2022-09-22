<template>
  <div class="canvas-container">
    <circle-button v-for="circle in circles" 
                   v-if="isAuthenticated"
                   :key="circle.id" 
                   :circle=circle 
                   @edit="onEditCircle" />
    <teleport to="#portal-target">
      <circle-edit v-if="editableCircle" 
                   :id="editableCircle.id"
                   @close="onEditClose"/>
    </teleport>
    <teleport to="#portal-target">
      <login-dialog v-if="!isAuthenticated"/>
    </teleport>
  </div>
</template>

<script setup lang="ts">

import {computed, ComputedRef, onMounted, ref, Ref} from "vue";
import {circleModule} from "@/store/circleModule";
import CircleButton from "@/components/CircleButton.vue";
import {Circle} from "@/models/Circle";
import CircleEdit from "@/views/CircleEdit.vue";
import {authModule} from "@/store/authModule";
import LoginDialog from "@/views/LoginDialog.vue";

let isAuthenticated: ComputedRef<boolean> = computed(() => authModule.IsAuthenticated);
let circles: ComputedRef<Circle[]> = computed(() => circleModule.Circles);

let editableCircle: Ref<Circle | null> = ref(null);

function onEditCircle(circle: Circle){
  editableCircle.value = circle;
}

function onEditClose(){
  editableCircle.value = null;
}

onMounted(  async () => {
  await authModule.validateToken();
});

</script>

<style scoped>
  .canvas-container{
    display: flex;
    width: 100%;
    height: 100%;
    position: absolute;
    background-color: transparent;
    overflow: auto;
  }
</style>