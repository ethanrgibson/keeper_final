<script setup>
import { AppState } from '@/AppState.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const keep = computed(() => AppState.activeKeep)
const myVaults = computed(() => AppState.myVaults)
const account = computed(() => AppState.account)

const editableData = ref(
  ''
)


async function createVaultKeep(keepId) {

  try {
    const vaultKeepData =
    {
      vaultId: editableData.value,
      keepId: keepId
    }


    await vaultKeepsService.createVaultKeep(vaultKeepData)

    editableData.value = ''

    Pop.toast('Keep Kept! 🤙🏻')

  }
  catch (error) {
    Pop.error(error);
  }

}

</script>


<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div v-if="keep" class="container-fluid">
          <div class="row">
            <div class="col-md-6 col-12 p-0 position-relative">
              <div class="exit-modal p-1 d-none" role="button" type="button" title="Exit Keep Details"
                data-bs-dismiss="modal">
                <span class="mdi mdi-alpha-x-box fs-5 text-white"></span>
              </div>
              <div>
                <img :src="keep.imgUrl" :alt="'Image uploaded by ' + keep.creator.name" class="rounded-keep keep-img">
              </div>
            </div>
            <div class="col-md-6 col-12">
              <div class="h-100 p-3 d-flex flex-column justify-content-between">
                <div class="d-flex justify-content-center gap-4">
                  <span class="fs-4 m-1 mdi mdi-eye" :title="keep.views + ' Views'">{{ keep.views }}</span>
                  <span class="fs-4 m-1 mdi mdi-alpha-k-box" :title="keep.kept + ' Times Kept'">{{ keep.kept }}</span>
                </div>
                <div class="text-center">
                  <span class="fw-bold fs-2 mb-2">{{ keep.name }}</span>
                  <p>{{ keep.description }}</p>
                </div>
                <div class="d-flex justify-content-between align-items-center gap-3">
                  <form v-if="account" @submit.prevent="createVaultKeep(keep.id)">
                    <div class="d-flex gap-2 align-items-center">
                      <select v-model="editableData" class="form-select" id="vaultId" required>
                        <option selected disabled value="">Add To A Vault</option>
                        <option v-for="vault in myVaults" :key="'Add to vault ' + vault.id" :value="vault.id">
                          {{ vault.name }}
                        </option>
                      </select>
                      <div>
                        <button type="submit" class="btn btn-indigo text-white" title="Save Keep to your Vault!">
                          Save
                        </button>
                      </div>
                    </div>
                  </form>
                  <div>
                    <RouterLink :to="{ name: 'Profile Page', params: { profileId: keep.creatorId } }">
                      <img :src="keep.creator.picture" :alt="'Profile Picture of ' + keep.creator.name"
                        class="profile-img me-2" data-bs-dismiss="modal">
                      <span>{{ keep.creator.name }}</span>
                    </RouterLink>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.keep-img {

  height: 80dvh;
  width: 100%;
  object-fit: cover;

}

.profile-img {
  max-height: 5dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

a {
  text-decoration: none;
  color: black;

}

.rounded-keep {
  border-radius: var(--bs-border-radius) 0 0 var(--bs-border-radius);
}

@media screen AND (max-width: 768px) {

  .exit-modal {
    display: block !important;
    position: absolute;
    top: 0;
    right: 0;
    text-shadow: 1px 1px 2px black;
  }

  .rounded-keep {
    border-radius: var(--bs-border-radius) var(--bs-border-radius) 0 0;
  }

}
</style>