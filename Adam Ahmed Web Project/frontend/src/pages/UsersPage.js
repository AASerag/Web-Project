import React, { useState, useEffect } from 'react';
import api from '../api/axios';

const UsersPage = () => {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const response = await api.get('/Auth/users', { withCredentials: true });
            setUsers(response.data);
        } catch (err) {
            setError("You do not have permission to view this page.");
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure you want to delete this user?")) return;
        try {
            await api.delete(`/Auth/users/${id}`, { withCredentials: true });
            fetchUsers(); // Refresh list
        } catch (err) {
            alert("Failed to delete user.");
        }
    };

    return (
        <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
            <h2>User Management (Admin Only)</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                <thead>
                    <tr style={{ backgroundColor: '#f8f9fa' }}>
                        <th style={{ padding: '10px', textAlign: 'left' }}>Username</th>
                        <th style={{ padding: '10px', textAlign: 'left' }}>Role</th>
                        <th style={{ padding: '10px', textAlign: 'left' }}>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(u => (
                        <tr key={u.id}>
                            <td style={{ padding: '10px', borderBottom: '1px solid #ddd' }}>{u.username}</td>
                            <td style={{ padding: '10px', borderBottom: '1px solid #ddd' }}>{u.role}</td>
                            <td style={{ padding: '10px', borderBottom: '1px solid #ddd' }}>
                                <button 
                                    onClick={() => handleDelete(u.id)}
                                    style={{ backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '5px 10px', cursor: 'pointer' }}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UsersPage;