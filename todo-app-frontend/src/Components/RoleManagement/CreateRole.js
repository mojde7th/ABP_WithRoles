import { useState } from "react"
import {createRole} from '../../services/authService'

const CreateRoleComponent=()=>
    {
const [roleName,setRoleName]=useState('');
const [message,setMessage]=useState('');

const handleCreateRole=async()=>{
    try{
await createRole(roleName);
setMessage(`Role '${roleName}' created
    successfully.`);
    setRoleName('');
    }
    catch(error){
        setMessage(`Failed to create
            role '${roleName}'.`);
    }
};

return(

<div className="role-management">
<h2>Create Role</h2>
<input type="text" value={roleName}
onChange={(e)=>setRoleName(e.target.value)}
placeholder="Role Name"
/>
<button onClick={handleCreateRole}>
    Create Role
</button>
{message && <p>{message}</p>}
</div>
);


};
export default CreateRoleComponent;