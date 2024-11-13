<template>
  <Card>
    <template #title>
        <span class="text-3xl p-font-bold">Account</span>
    </template>
    <template #content>
        <div class="flex flex-col gap-2">
            <IftaLabel>
                <InputText class="mr-1" id="username" v-model="value" variant="filled" :disabled="isDisabled" :value="artistUsername" :invalid="isInvalid" />
                <label for="username">Username</label>
                <Button class="p-button p-button-rounded mr-1" :icon="editIcon" @click="toggleInput()" />
                <Button class="p-button p-button-rounded mr-1" icon="pi pi-check" v-if="!isDisabled" @click="updateUsername()" />
            </IftaLabel>
        </div>
        <Message v-if="isTooLong" size="small" severity="error" variant="simple">Username is too long. Max of 16 characters.</Message>
        <Message v-if="isTaken" size="small" severity="error" variant="simple">Username is already taken.</Message>
        <div class="py-2"> <Button label="logout" icon="pi pi-sign-out" @click="logout()" /></div>
    </template>
  </Card>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import LoginService from "@/services/LoginService";
import router from "@/router";
import { useToast } from "primevue/usetoast";
import Card from "primevue/card";
    import Button from "primevue/button";
    import InputText from 'primevue/inputtext';
    import IftaLabel from 'primevue/iftalabel';

    const toast = useToast();
    var isDisabled = ref(true);
    var isInvalid = ref(false);
    var isTooLong = ref(false);
    var isTaken = ref(false);
    var artistUsername;
    var editIcon = "pi pi-pencil";


onMounted(() => {
  LoginService.isLoggedIn().then((isLoggedIn) => {
    if (!isLoggedIn) {
      router.push("/");
      toast.add({
        severity: "error",
        summary: "Warning",
        detail: "User must be logged in to view account page",
        life: 3000,
      });
      }

      (async () => {
          toggleInput();
          artistUsername = await LoginService.getUsername();
          toggleInput();
      })()  
  });
});

function logout() {
  LoginService.logout().then(() => {
    window.location.replace(`/`);
    toast.add({
      severity: "success",
      summary: "Success",
      detail: "User logged out",
      life: 3000,
    });
  });
    }

    function toggleInput() {
        isDisabled.value = !isDisabled.value;
        isInvalid.value = false;
        isTooLong.value = false;
        isTaken.value = false;

        (async () => {
            artistUsername = await LoginService.getUsername();
            (<HTMLInputElement>document.getElementById('username')).value = artistUsername;
        })()

        if (isDisabled.value) {
            editIcon = "pi pi-pencil";
        }
        else {
            editIcon = "pi pi-times";
        }
    }

    function getUsername() {
        return LoginService.getUsername();
    }

    function updateUsername() {
        var inputText = (<HTMLInputElement>document.getElementById('username'));
        var newUsername = inputText.value;
        var rowsChanged;

        if ((newUsername as string).length > 16) {
            isInvalid.value = true;
            isTooLong.value = true;
            isTaken.value = false;
            artistUsername = newUsername;
            // toggleInput()
        }
        else {
            isInvalid.value = false;
            isTooLong.value = false;
            (async () => {
                rowsChanged = await LoginService.updateUsername(newUsername);

                if (rowsChanged == 0) {
                    isTaken.value = true;
                }
                else {
                    isTaken.value = false;
                    isDisabled.value = true;
                    artistUsername = newUsername;
                    editIcon = "pi pi-pencil";
                    toast.add({
                        severity: "success",
                        summary: "Success",
                        detail: "Username successfully changed.",
                        life: 3000,
                    });
                }
            })()
        }
    }
</script>   
