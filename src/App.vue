<template>
  <div id="app">
    <Header />
    <router-view/>
  </div>
</template>

<script>
import Header from "@/components/Header";
export default {
  name: 'App',
  components:{
    Header: Header
  }
};
</script>

<style>
body,
html {
  width: 100%;
  height: 100%;
  padding: 0;
  margin: 0;
}
</style>
