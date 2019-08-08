<template>
<div class="signup">
    <AuthPanel type="signup" @auth-panel-submit="handleSignUp" />
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
      handleSignUp: function (payload) {
        const that = this;
        const signupUrl = "https://localhost:5001/api/signup";
        axios
          .post(signupUrl, payload)
          .then(function(res) {
            console.log("Signup Success");
            console.log(res.data);
            that.$router.push("/login");
          })
          .catch(function (err) {
            console.error(err.response.data.errors);
          });
      }
    }
};
</script>

<style>
.signup{
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    padding-top: 100px;
}
</style>
