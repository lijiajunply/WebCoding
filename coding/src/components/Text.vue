<template>

  <div id="app">
    <label>C++<input type="checkbox" v-model="gender" value="C++"/></label>
    <label>CSharp<input type="checkbox" v-model="gender" value="CSharp"/></label>
    <label>Java<input type="checkbox" v-model="gender" value="Java"/></label>
    <label>Python<input type="checkbox" v-model="gender" value="Python"/></label>
    <label>C<input type="checkbox" v-model="gender" value="C"/></label>
    <label>Python2<input type="checkbox" v-model="gender" value="Python"/></label>
  </div>

  <codemirror v-model="code"
              :style="{ height: '100%' }"
              :autofocus="true"
              :tabSize="2"
              :extensions="extensions"/>
  <button @click="f()" >Run</button>
  <label>{{result}}</label>
</template>
<script lang="ts" setup>
import { Codemirror } from "vue-codemirror";
import { javascript } from "@codemirror/lang-javascript";
import { oneDark } from "@codemirror/theme-one-dark";
import { ref } from "vue";
import {java} from "@codemirror/lang-java";
import {cpp} from "@codemirror/lang-cpp";
import Axios from "axios";
let gender = "";
let result = "";
console.log(gender);
const code = ref(``);
const extensions = [javascript(), oneDark];

function f() {
  window.axios.post("https://localhost:7045/api/Debug",window.qs.stringify({
    'code' : code,
    'lang' : gender}
  )).then(function (res) {
    if (res.data.code == 200) {
      result = res.data.data;
    }
  }).catch(function (error) {
    console.log(error);
  });
}

</script>