<template>
<div class="header">
  <h1 @click="handleTitleClick">{{ title }}</h1>
  <p class="new-photo-btn btn" @click="handleNewPhotoClick">New Photo</p>
  <div class="user-unlogin-container" v-if="!isLogin">
    <p class="login-btn btn" @click="handleLogin">Login</p>
    <p class="signup-btn btn" @click="handleSignUp">Sign up</p>
  </div>
  <div class="user-login-container" v-else>
    <p class="user-email">{{ userEmail }}</p>
    <p class="login-btn btn" @click="handleLogout">Log out</p>
  </div>
</div>
</template>

<script>
export default{
    data(){
        return {
            title: "Photo Album",
            isLogin: false,
            userEmail: "alphcamp@mail.com"
        };
    },
    methods:{
        handleTitleClick: function(){
            this.$router.push("/");
        },
        handleNewPhotoClick: function(){
            this.$router.push("/photos/new");
        },
        handleSignUp: function(){
            this.$router.push("/signup");
        },
        handleLogin: function(){
            this.$router.push("/login");
        },
        handleLogout: function(){
            console.log("logout");
        }
    }
};

</script>

<style scoped>
.header {
  height: 60px;
  width: 100%;
  background-color: #e7e7e7;
  border-bottom: 2px solid #c0c0c0;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.header h1 {
  font-size: 26px;
  padding-left: 25px;
  margin: 0;
  color: #404040;
}
.header h1:hover {
  cursor: pointer;
  color: #000;
}
.user-email {
  font-size: 18px;
  margin-right: 30px;
}
.new-photo-btn {
  font-size: 18px;
  font-weight: 500;
  color: #5e5e5e;
  margin-left: 30px;
  margin-right: auto;
}
.user-unlogin-container {
  height: 100%;
  width: 200px;
  display: flex;
  justify-content: space-around;
  align-items: center;
}
.user-login-container p {
  font-size: 18px;
  font-weight: 500;
  color: #5e5e5e;
}
.user-login-container {
  height: 100%;
  width: auto;
  margin-right: 15px;
  display: flex;
  justify-content: space-around;
  align-items: center;
}
.user-unlogin-container p {
  font-size: 18px;
  font-weight: 500;
  color: #5e5e5e;
}
.btn:hover {
  cursor: pointer;
  color: #000;
}
@media (max-width: 772px) {
  .header h1 {
    display: none;
  }
}
</style>