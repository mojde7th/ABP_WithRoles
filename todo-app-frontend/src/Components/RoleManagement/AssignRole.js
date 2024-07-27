import { useState } from "react";
import { assignRole } from "../../services/authService";



const AssignRoleComponent=()=>{
const [username,setUsername]=useState('');
const [roleName,setRoleName]=useState('');
const [message,setMessage]=useState('');

const handleAssignRole=async()=>{
    try{
await assignRole(username,roleName);
setMessage(`Role '${roleName}'
    assigned to '${username}'
    successfully.`);
    setUsername('');
    setRoleName('');
    }
    catch(error){
setMessage(`Failed to assign role
    '${roleName}'
    to '${username}'.`);
    }
}
return(
    <div className="role-management">
<h2>Assign Role</h2>
<input type="text" value={username}
onChange={(e)=>setUsername(e.target.value)}
placeholder="Username"
/>
<input type="text" value={roleName}
onChange={(e)=>{setRoleName(e.target.value)}}
placeholder="Role Name"/>
<button onClick={handleAssignRole}>
    Assign Role
</button>
{message && <p>{message}</p>}
    </div>
);
};

export default AssignRoleComponent;