import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import api from '../api/axios';
import { toast } from 'react-toastify';
import { 
    Container, Paper, Box, Typography, TextField, 
    Button, Avatar, Grid, Link as MuiLink, Alert,
    FormControl, InputLabel, Select, MenuItem 
} from '@mui/material';
import AppRegistrationIcon from '@mui/icons-material/AppRegistration';

const RegisterPage = () => {
    const [formData, setFormData] = useState({ 
        username: '', 
        password: '', 
        role: 'User' 
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleRegister = async (e) => {
    e.preventDefault();
    try {
        await api.post('/Auth/register', formData);
        
        // Success Notification
        toast.success("Account created successfully! Redirecting to login...");
        
        setTimeout(() => {
            navigate('/'); 
        }, 2000);
    } catch (err) {
        // Error Notification
        const errorMsg = err.response?.data || "Registration failed. Try a different username.";
        toast.error(errorMsg);
    }
    };

    return (
        <Container maxWidth="xs">
            <Box sx={{ mt: 8, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <Paper elevation={6} sx={{ p: 4, display: 'flex', flexDirection: 'column', alignItems: 'center', borderRadius: 3 }}>
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <AppRegistrationIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5" fontWeight="bold">
                        Create Account
                    </Typography>
                    
                    {error && <Alert severity="error" sx={{ mt: 2, width: '100%' }}>{error}</Alert>}

                    <Box component="form" onSubmit={handleRegister} sx={{ mt: 3 }}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            label="Username"
                            autoFocus
                            value={formData.username}
                            onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            label="Password"
                            type="password"
                            value={formData.password}
                            onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                        />
                        
                        {/* Added Role Selection to match your Backend requirements */}
                        <FormControl fullWidth sx={{ mt: 2 }}>
                            <InputLabel id="role-label">System Role</InputLabel>
                            <Select
                                labelId="role-label"
                                value={formData.role}
                                label="System Role"
                                onChange={(e) => setFormData({ ...formData, role: e.target.value })}
                            >
                                <MenuItem value="User">User (Staff)</MenuItem>
                                <MenuItem value="Admin">Administrator</MenuItem>
                            </Select>
                        </FormControl>

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2, py: 1.5, fontWeight: 'bold' }}
                        >
                            Register
                        </Button>
                        
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                {/* FIXED: "to" points to the root path where login lives */}
                                <MuiLink component={Link} to="/" variant="body2" sx={{ cursor: 'pointer' }}>
                                    Already have an account? Sign in
                                </MuiLink>
                            </Grid>
                        </Grid>
                    </Box>
                </Paper>
            </Box>
        </Container>
    );
};

export default RegisterPage;