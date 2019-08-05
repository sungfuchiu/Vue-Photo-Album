<template>
<div class="signup">
    <AuthPanel type="signup" />
</div>
</template>

<script>
import AuthPanel from "@/components/AuthPanel";
export default{
    components:{
        AuthPanel: AuthPanel
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