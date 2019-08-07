<template>
<div class="login">
    <AuthPanel type="login" @auth-panel-submit="handleLogin" />
</div>
</template>

<script>
  import AuthPanel from "@/components/AuthPanel";
  import axios from "axios";
export default{
    components:{
        AuthPanel: AuthPanel
    },
    methods: {
      handleLogin: function (payload) {
        console.log("handleLogin");
        var that = this;
        var url = "https://localhost:44316/api/login";
        axios
          .post(url, payload)
          .then(function (res) {
            console.log("login success!");
            console.log(res.data.auth_token);

            var authToken = res.data.auth_token;
            var email = payload.email;
            var sessionData = { authToken: authToken, email: email };
            localStorage.setItem("photo-album-user", JSON.stringify(sessionData));

            that.$bus.$emit("auth-state", { action: "login" });
            that.$router.push("/");
          })
          .catch(function (err) {
            console.error(err.response.data.errors);
          });
      }
    }
};
</script>

<style scoped>
.login{
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    padding-top:100px;
}
</style>
