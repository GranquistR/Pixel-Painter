<template>
    <FloatingCard position="bottom"
                  header="Frames"
                  button-icon="pi pi-images"
                  button-label=""
                  width=""
                  :default-open="true">

        <Button class="mr-1" icon="pi pi-minus" size="small" rounded @click="removeFrame()" />

        <template v-for="frame in frames" :key="frame.id">
            <Button :icon="frame.icon" 
                    :class="frame.class" 
                    :severity="frame.severity"
                    @click="switchFrame(frame.id)" 
                    />
        </template>

        <Button class="ml-1" icon="pi pi-plus" size="small" rounded @click="addFrame()" />
    </FloatingCard>
</template>

<script setup lang="ts">
import Button from "primevue/button";
import Tag from "primevue/tag";
import FloatingCard from "./FloatingCard.vue";
import { ref, onBeforeMount } from 'vue';
import { useLayerStore } from "@/store/LayerStore.ts"
import { PixelGrid } from "@/entities/PixelGrid.ts"

    let selectedFrame = defineModel<number>("selFrame", { default: 0 });
    //let oldFrame = defineModel<number>("lastFrame", { default: 1 });
    //let index = defineModel<number>("frameIndex", { default: 2 });

    const frameStore = useLayerStore();
    // const selFrame = ref<number>(0);
    // const frames = ref<number[]>([0]);

    let frameCount = 1;
    const frames = ref([
        { id: 0, icon: 'pi pi-image', class: 'm-1', severity: 'secondary' }
    ]);

    onBeforeMount(() => {
        //var count = 2;
        //while (localStorage.getItem(`frame${count}`) != null) {
        //    frames.value.push({
        //        id: count,
        //        icon: 'pi pi-image',
        //        class: 'mr-1',
        //        severity: 'secondary'
        //    });

        //    frameCount++;
        //    count++;
        //    index.value++;
        //}
        for (let i = 1; i < frameStore.grids.length; i++) {
            frames.value.push({
                    id: i,
                    icon: 'pi pi-image',
                    class: 'mr-1',
                    severity: 'secondary'
             });
        }

        frames.value[0].severity = 'primary';
    });

    function addFrame() {
        //frameCount++;
        //index.value++;
        frames.value.push({
            id: frameStore.grids.length,
            icon: 'pi pi-image',
            class: 'mr-1',
            severity: 'secondary'
        });
        frameStore.pushGrid(new PixelGrid(
            frameStore.grids[0].height,
            frameStore.grids[0].height,
            frameStore.grids[0].backgroundColor,
            frameStore.grids[0].isGif)
        );
    }

    function removeFrame() {
        if (frames.value.length > 1) {
            frames.value.pop();
            frameStore.popGrid();
            //frameCount--;
            //index.value--;

            if (selectedFrame.value == frameCount + 1) {
                localStorage.getItem(`frame${frameCount}`)
            }
        }

        if (localStorage.getItem(`frame${frameCount + 1}`) != null) {
            localStorage.removeItem(`frame${frameCount + 1}`);
        }
    }

    function switchFrame(frameID: number) {
        frames.value.forEach((nFrame) => {
            nFrame.severity = 'secondary';
        });
        frames.value[frameID].severity = 'primary';

        //oldFrame.value = selectedFrame.value;
        //selectedFrame.value = frameID;

        selectedFrame.value = frameID;
        frameStore.layer = frameID;
    }

</script>

<style>
    .p-dialog.p-component {
        margin-bottom: 75px !important;
    }
</style>