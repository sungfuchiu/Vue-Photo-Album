<template>
<div class="container">
    <div class="item-list" v-for="(photo, index) in photos" :key="index">
        <itemListElement />
    </div>
</div>
</template>

<script>
import ItemListElement from "@/components/ItemListElement";
export default {
    data: function(){
        return {
            isLogin: false,
            photos: [1,2,3,4,5]
        };
    },
    components:{
        ItemListElement: ItemListElement
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