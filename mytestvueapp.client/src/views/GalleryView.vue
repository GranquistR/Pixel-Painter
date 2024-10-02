<script setup lang="ts">
    import { onMounted, ref } from "vue";
    import ArtCard from "../components/ArtCard.vue"
    import Art from "@/entities/Art";
    import ArtAccessService from "@/utils/ArtAccessService";

    const stuff = ref<Art[] | null>(null); // Local call
    const stuff2 = ref<IterableIterator<Art> | null>(null); // Service call

    //ArtAccessService.getAllArt().then((r) => stuff2.value = r);

    onMounted(() => {
        fetchData();
        //ArtAccessService.getAllArt().then((promise) => stuff2.value = promise.values);
    });

    function fetchData() {
        stuff.value = null;

        fetch("artaccess/GetAllArt")
            .then((r) => r.json())
            .then((json) => {
                stuff.value = json as Art[];
                return;
            })
            .catch(console.error)
    }
</script>


<template>
    <div class="w-9 mx-auto my-0">
        <div class="flex flex-wrap">
            <ArtCard v-for="art in stuff" :key="art.artId" :art="art" />
            <ArtCard v-for="art in stuff2" :key="art.artId" :art="art" />
        </div>
        <div>
            {{ stuff2 }}
        </div>
    </div>
</template>