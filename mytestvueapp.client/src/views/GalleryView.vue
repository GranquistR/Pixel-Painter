<template>
  <div class="w-9 mx-auto my-0">
    <header>
        <h1 class="flex align-items-center gap-3">
            Search for Art
            <InputText class="mt-2"
                       v-model.trim="search"
                       type="text"
                       placeholder="Search..." />
            <Dropdown class="pl mt-2 text-base w-1.5 font-normal"
                      v-model="sortType"
                      :options="sortBy"
                      optionLabel="sort"
                      optionValue="code"
                      placeholder="Sort by" />
            <ToggleButton v-if="isSorted"
                          id="toggle"
                          class="mt-2 text-base w-0 font-normal"
                          v-model="checkAscending"
                          onLabel="Ascending"
                          onIcon="pi pi-arrow-up"
                          offLabel="Descending"
                          offIcon="pi pi-arrow-down"
                          @click="handleCheckBox()" />

            <ToggleButton v-if="isSortedByDate"
                          id="toggle"
                          class="mt-2 text-base w-0 font-normal"
                          v-model="checkAscending"
                          onLabel="Oldest First"
                          onIcon="pi pi-arrow-up"
                          offLabel="Newest First"
                          offIcon="pi pi-arrow-down"
                          @click="handleCheckBox()" />
        </h1>
    </header>
    <div class="shrink-limit flex flex-wrap" v-if="!loading">
      <ArtCard v-for="art in displayArt" :key="art.id" :art="art" :size="6"/>
    </div>
  </div>
</template>


<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/services/ArtAccessService";
import InputText from "primevue/inputtext";
    import Dropdown from 'primevue/dropdown';
    import Checkbox from 'primevue/checkbox';
    import ToggleButton from 'primevue/togglebutton';


const publicArt = ref<Art[]>();
const displayArt = ref<Art[]>();
const search = ref("");
const loading = ref(true);
    const sortBy = ref([
        { sort: 'Likes', code: 'L'},
        { sort: 'Comments', code: 'C' },
        { sort: 'Date', code: 'D' }
    ]);
    const sortType = ref(""); // Value binded to sort drop down
    const isSorted = ref(false); // Renders the Descending checkbox while true
    const isSortedByDate = ref(false);
    const checkAscending = ref(false);

onMounted(() => {
    ArtAccessService.getAllArt() // Get All Art
    .then((data) => {
      publicArt.value = data;
      displayArt.value = data;
    })
    .finally(() => {
      loading.value = false;
    });
});

watch(search, () => {
    if (publicArt.value) {
        displayArt.value = publicArt.value.filter((Art) =>
        Art.title.toLowerCase().includes(search.value.toLowerCase())
    );
    }
});

    watch(sortType, () => {
        sortGallery();
    });

    function handleCheckBox() { // Gets called when the ascending checkbox is clicked
        sortGallery();
    }

    function sortGallery() {
        //isSorted.value = true;
        var sortCode = sortType.value;

        if (sortCode == 'D') {
            isSorted.value = false;
            isSortedByDate.value = true;
        }
        else {
            isSorted.value = true;
            isSortedByDate.value = false;
        }

        switch (sortCode) {
            case "L": { // Likes
                ArtAccessService.getArtByLikes(checkAscending.value).then((data) => {
                    publicArt.value = data;
                    displayArt.value = data;
                })
                    .finally(() => {
                        loading.value = false;
                        if (publicArt.value) {
                            displayArt.value = publicArt.value.filter((Art) =>
                                Art.title.toLowerCase().includes(search.value.toLowerCase())
                            );
                        }
                    });
                break;
            }
            case "C": { // Commments
                ArtAccessService.getArtByComments(checkAscending.value).then((data) => {
                    publicArt.value = data;
                    displayArt.value = data;
                })
                    .finally(() => {
                        loading.value = false;
                        if (publicArt.value) {
                            displayArt.value = publicArt.value.filter((Art) =>
                                Art.title.toLowerCase().includes(search.value.toLowerCase())
                            );
                        }
                    });
                break;
            }
            case "D": { // Date
                ArtAccessService.getArtByDate(checkAscending.value).then((data) => {
                    publicArt.value = data;
                    displayArt.value = data;
                })
                    .finally(() => {
                        loading.value = false;
                        if (publicArt.value) {
                            displayArt.value = publicArt.value.filter((Art) =>
                                Art.title.toLowerCase().includes(search.value.toLowerCase())
                            );
                        }
                    });
                break;
            }
        }
    }
</script>
