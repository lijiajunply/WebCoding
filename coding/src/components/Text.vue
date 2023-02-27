<template>
  <!-- :indent-with-tab="true" 是否自动获取焦点-->
  <codemirror v-model="code"
              placeholder="Code gose here..."
              :style="{ height: '100%' }"
              :autofocus="true"
              :tabSize="2"
              :extensions="extensions"/>
</template>
<script lang="ts" setup>
import { Codemirror } from "vue-codemirror";
import { javascript } from "@codemirror/lang-javascript";
import { oneDark } from "@codemirror/theme-one-dark";
import { ref } from "vue";
import {java} from "@codemirror/lang-java";
interface IProps {
  height?: string,
}
// 接受的参数
const props = withDefaults(defineProps<IProps>(), {
  height: '400px'
})
const code = ref(``);
const extensions = [java(), oneDark];
const Change = () => {

}
</script>