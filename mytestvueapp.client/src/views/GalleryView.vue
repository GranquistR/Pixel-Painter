<script setup>
    import { ref } from "vue";
    import artworkData from "../dummydata/DummyWorksOfArt"
    import ArtCard from "../components/ArtCard.vue"

    const artworks = ref(artworkData)
</script>


<template>
    <div class="w-8 mx-auto my-0">
        <div class="flex">
            <ArtCard v-for="art in artworks" :key="art.id" :art="art"/>
        </div>
    </div>
</template>