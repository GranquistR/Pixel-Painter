
<template>

    <div class="p-8">
        <my-canvas >
            <MyBox>

            </MyBox>
        </my-canvas>
    </div>
    <div class = "border-solid border-1" >
        <h2>"this is some text"</h2>
    </div>
</template>
<script setup lang="ts">
    import MyCanvas from '@/components/MyCanvas/MyCanvas.vue';
    import MyBox from '@/components/MyCanvas/MyBox.vue';
    import Button from "primevue/button";

</script>

