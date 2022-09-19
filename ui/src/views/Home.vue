<template>
  <div class="canvas">
    <circle-button v-for="circle in circles" :key="circle.id" :circle=circle @edit="onEditCircle" />
    <teleport to="#portal-target">
      <circle-edit v-if="editableCircle" 
                   :id="editableCircle?.id"
                   v-model:description="editableCircleDescription"/>
    </teleport>
  </div>
</template>

<script setup lang="ts">

import {computed, ComputedRef, onMounted, ref, Ref} from "vue";
import {circleModule, CircleModule} from "@/store/CircleModule";
import CircleButton from "@/components/CircleButton.vue";
import {Circle} from "@/models/Circle";
import CircleEdit from "@/views/CircleEdit.vue";

let circles: ComputedRef<Circle[]> = computed(() => circleModule.Circles);
let editableCircle: ComputedRef<Circle | undefined> = computed(() => circleModule.EditableCircle);

let editableCircleDescription = computed({
  get: () => {
    if (!editableCircle || !editableCircle.value) return "";
    return editableCircle.value.description;
  },
  set: (value: string) => {
    circleModule.saveEditableCircleDescriptionMutation(value);
  }
});

function onEditCircle(circle: Circle){
  circleModule.circleEditMutation(circle);
}

onMounted(  async () => {
  circleModule.addCircleMutation();
});

</script>

<style scoped>
  .canvas{
    display: flex;
    width: 100vw;
    height: 100vh;
    position: absolute;
    background-color: transparent;
    overflow: auto;
  }
</style>