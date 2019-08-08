<template>
<div class="show">
    <div class="photo-container">
        <ImgDisplay :url="url" v-if="url!='' && url != null"/>
    </div>
    <div class="main-container">
        <h3 class="main-container"></h3>
        <p class="main-date">{{date}}</p>
        <p class="main-description">{{description}}</p>
    </div>
</div>
</template>

<script>
import ImgDisplay from "@/components/ImgDisplay";
import axios from "axios";
export default{
    data: function(){
        return {
            title: "title",
            description:"Lorem ipsum, dolor sit amet consectetur adipisicing elit. Dignissimos, cupiditate.",
            url: "",
            date: "2018-01-01"
        };
    },
    components:{
        ImgDisplay: ImgDisplay
    },
    created: function(){
      var that = this;
      var id = this.$route.params.id;
      var showUrl = "https://localhost:5001/api/Photos/" + id;

      if(localStorage.getItem("photo-album-user")){
        var token = JSON.parse(localStorage.getItem("photo-album-user"))
                    .authToken;
        var params = { auth_token: token};
      }
      axios
        .get(showUrl)
        .then(function(res){
          that.title = res.data.title;
          that.date = res.data.date;
          that.description = res.data.description;
          that.url = "https://localhost:5001/" + res.data.file_location.url;
        })
        .catch(function(err){
          console.error(err.response.data);
          that.$router.push("/login");
        })
    }
};
</script>

<style scoped>
.show {
  display: flex;
  justify-content: center;
  align-items: center;
}
.photo-container {
  padding-top: 100px;
  padding-right: 50px;
}
.main-container {
  width: 500px;
  padding-top: 100px;
  margin-bottom: auto;
}
.main-title {
  font-size: 43px;
  margin-top: 0;
  margin-bottom: 0;
}
.main-date {
  font-size: 24px;
}
.main-description {
  font-size: 20px;
}
</style>