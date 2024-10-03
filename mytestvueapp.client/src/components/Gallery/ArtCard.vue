<script setup>

const { art } = defineProps(['art'])
</script>

<template> 
    <div class="flex-shrink-0 w-13rem overflow-hidden border-round-sm mr-4 mb-4 cursor-pointer bg-white shadow-2">
        <img class="w-full h-10rem" :src="art.encode">
        <div class="flex flex-column flex-column justify-content-between flex-nowrap px-1 py-1">
            <p class="text-base font-bold">{{ art.artName }}</p>
            <p class="text-sm">{{ art.artistName }}</p>
        </div>
    </div>
</template>

<style scoped>
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }
</style>