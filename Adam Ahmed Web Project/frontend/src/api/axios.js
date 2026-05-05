import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5075/api', // Keep whatever your URL currently is
    withCredentials: true // THIS IS THE MAGIC LINE! It tells React to send the HttpOnly cookie.
});

export default api;