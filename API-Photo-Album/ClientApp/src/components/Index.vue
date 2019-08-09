<template>
<div class="container">
    <div class="item-list" v-for="(photo, index) in photos" :key="photo.id">
        <itemListElement
          :isLogin="isLogin"
          :id="photo.id"
          :title="photo.title"
          :description="photo.description"
          :url="'https://localhost:5001/'+photo.file_location.url"
          @destroy-item="handleDestroyItem(index)"/>
    </div>
</div>
</template>

<script>
import ItemListElement from "@/components/ItemListElement";
import axios from "axios";
export default {
    data: function(){
        return {
            isLogin: false,
            photos: []
        };
    },
    components:{
        ItemListElement: ItemListElement
    },
    methods: {
      handleAuthState: function (payload) {
        var action = payload.action;
        if (action == "login") {
          this.isLogin = true;
        } else if (action == "logout") {
          this.isLogin = false;
        }
      },
      handleDestroyItem: function(index){
        console.log("delete photo "+index);
        this.photos.splice(index, 1);
      }
    },
    created() {
      this.$bus.$on("auth-state", this.handleAuthState);
      var sessionData = JSON.parse(localStorage.getItem("photo-album-user"));
      if (!!sessionData) {
        this.handleAuthState({ action: "login" });
      } else {
        this.handleAuthState({ action: "logout" });
      }

      var indexUrl = "https://localhost:5001/api/Photos";
      var hostUrl = "https://localhost:5001/";
      var that = this;
      axios
        .get(indexUrl, {})
        .then(function (res) {
          console.dir(res.data.data);
          that.photos = res.data.data;
        })
        .catch(function (err) {
          console.error(err.response.data);
        })
    },
    beforeDestroy() {
      this.$bus.$off("auth-state", this.handleAuthState);
    }
};
</script>

<style scoped>
.container {
  max-width: 1440px;
  margin: 100px auto;
  display: flex;
  flex-wrap: wrap;
}
.item-list {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 25%;
  margin-bottom: 60px;
}
@media (max-width: 993px) {
  .photo-container-wrapper {
    width: 33.333%;
  }
}
@media (max-width: 769px) {
  .photo-container-wrapper {
    width: 50%;
  }
}
@media (max-width: 577px) {
  .photo-container-wrapper {
    width: 100%;
  }
}
</style>
