<template>
<div class="edit">
  <div class="photo-container">
    <ImgDisplay/>
  </div>
  <div class="main-container">
    <PhotoForm/>
  </div>
</div>
</template>

<script>
import ImgDisplay from "@/components/ImgDisplay";
import PhotoForm from "@/components/PhotoForm";
export default {
  components: {
    ImgDisplay: ImgDisplay,
    PhotoForm: PhotoForm
  },
  data: function() {
    return {
      photo: {}
    };
  }
};
</script>

<style scoped>
.edit {
  display: flex;
  justify-content: center;
}
.photo-container {
  padding-top: 100px;
  margin-right: 50px;
}
.main-container {
  padding-top: 100px;
}
</style>