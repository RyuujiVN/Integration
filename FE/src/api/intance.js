import axios from "axios"

const instance = axios.create({
  baseURL: "http://localhost:5140/api",
  // withCredentials: true,
  // timeout: 10 * 60 * 1000
})

export default instance