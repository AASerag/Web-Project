import React, { useState, useEffect } from 'react';
import api from '../api/axios';
import { toast } from 'react-toastify';
import { Container, Paper, Box, Typography, TextField, Button, Avatar } from '@mui/material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        // Clear local storage on load to prevent state mismatch
        localStorage.clear(); 
    }, []);

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post('/Auth/login', { username, password });
            
            localStorage.setItem('userRole', response.data.role);
            localStorage.setItem('isAuthenticated', 'true');
            
            toast.success(`Logged in as ${response.data.username}`);
            
            // Force a clean reload to sync App.js state
            setTimeout(() => {
                window.location.href = '/dashboard'; 
            }, 1000);
        } catch (err) {
            toast.error("Invalid username or password");
        }
    };

    return (
        <Container maxWidth="xs">
            <Box sx={{ mt: 8, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <Paper elevation={6} sx={{ p: 4, borderRadius: 3, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <Avatar sx={{ m: 1, bgcolor: 'primary.main' }}><LockOutlinedIcon /></Avatar>
                    <Typography component="h1" variant="h5" fontWeight="bold">Staff Login</Typography>
                    <Box component="form" onSubmit={handleLogin} sx={{ mt: 1 }}>
                        <TextField
                            margin="normal" required fullWidth label="Username"
                            autoFocus value={username} onChange={(e) => setUsername(e.target.value)}
                        />
                        <TextField
                            margin="normal" required fullWidth label="Password"
                            type="password" value={password} onChange={(e) => setPassword(e.target.value)}
                        />
                        <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2, py: 1.5, fontWeight: 'bold' }}>
                            Sign In
                        </Button>
                    </Box>
                </Paper>
            </Box>
        </Container>
    );
};

export default LoginPage;