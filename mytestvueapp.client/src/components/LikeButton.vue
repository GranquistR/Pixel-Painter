<script setup lang="ts">
    import Button from "primevue/button";
    import LoginService from "@/services/LoginService";
    import LikeService from "@/services/LikeService";

    import { ref, onMounted } from "vue";
    
    const props = defineProps<{ 
        likes: number;
        artId: number;
    }>();

    const localLike = ref(0);

    const liked = ref(false);
    const loggedIn = ref(false);

  onMounted(() => {
    LikeService.isLiked(props.artId)
    .then((value) => liked.value = value);

    LoginService.isLoggedIn()
    .then((value) => loggedIn.value = value);

    localLike.value = 0;
});

    const likedClicked = () => {
      console.log("Like Clicked");
      if (!loggedIn.value) {
        // Route to login page
        console.log("User is not loggged in!");
        return;
      }
      if (liked.value) {
        // Try to unlike
        console.log("Trying to unlike");
        LikeService.removeLike(props.artId)
        .then((value) => {
            if (value) {
                liked.value = false;
            } 
            if (localLike.value >= 0) {
                localLike.value--;
            }
        
        });
      } else {
        // Try to Like
        console.log("Trying to like");
        LikeService.insertLike(props.artId)
        .then((value) => {
            console.log("insertLike()", value);
            if (value) {
                liked.value = true;
            } 
            if (localLike.value <= 0) {
                localLike.value++;
            }
            
        });
      }
      // Calculate new number of likes

    }
</script>

<template>
    <Button
        :severity="liked ? 'danger' : ''"
        class="w-full flex-grow p-1"
        :icon="liked ? 'pi pi-heart-fill' : 'pi pi-heart'"
        :label="likes + localLike"
        @click.stop="likedClicked()"
    />
</template>

<style scoped>
    
</style>
