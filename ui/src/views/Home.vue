<template>
  <div class="canvas-container">
    <circle-button v-for="circle in circles" 
                   v-if="isAuthenticated"
                   :key="circle.id" 
                   :circle=circle 
                   @edit="onEditCircle" />
    <teleport to="#portal-target">
      <circle-edit v-if="editableCircle" 
                   :id="editableCircle?.id"
                   v-model:description="editableCircleDescription"/>
    </teleport>
    <teleport to="#portal-target">
      <login-dialog v-if="!isAuthenticated"/>
    </teleport>
  </div>
</template>

<script setup lang="ts">

import {computed, ComputedRef, onMounted} from "vue";
import {circleModule} from "@/store/circleModule";
import CircleButton from "@/components/CircleButton.vue";
import {Circle} from "@/models/Circle";
import CircleEdit from "@/views/CircleEdit.vue";
import {authModule} from "@/store/authModule";
import LoginDialog from "@/views/LoginDialog.vue";

let isAuthenticated: ComputedRef<boolean> = computed(() => authModule.IsAuthenticated);

let circles: ComputedRef<Circle[]> = computed(() => circleModule.Circles);
let editableCircle: ComputedRef<Circle | undefined> = computed(() => circleModule.EditableCircle);

let editableCircleDescription = computed({
  get: () => {
    if (!editableCircle || !editableCircle.value) return "";
    return editableCircle.value.description;
  },
  set: async (value: string) => {
    if (editableCircle.value){
      await circleModule.setDescription({id: editableCircle.value.id, newDescription: value});
    }
  }
});

function onEditCircle(circle: Circle){
  circleModule.circleEditMutation(circle);
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