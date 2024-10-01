<script setup>
    import { ref } from "vue";
    import artworkData from "../dummydata/DummyArt"
    import ArtCard from "../components/ArtCard.vue"
    import Art from "@/entities/Art";
    import ArtAccessService from "@/utils/ArtAccessService";

    

    const stuff = ref(ArtAccessService.getAllArt());

    const artworks = ref(artworkData)
</script>


<template>
    <div class="w-8 mx-auto my-0">
        <div class="flex">
            <ArtCard v-for="art in artworks" :key="art.id" :art="art"/>
        </div>
        {{ stuff }}
    </div>
</template>