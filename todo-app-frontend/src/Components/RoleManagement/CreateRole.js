import { useState } from "react"
import {createRole} from '../../services/authService'
import { Link } from "react-router-dom";

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
<Link to={`/publishers`}>Back to publishers Page</Link>

      {/* <Link to={`/roles/create`}>Back to CreateRole Page</Link> */}
      <br/>
      <Link to="/publishers/new">Back to Adding new Publishers</Link>
      <br/>
      <Link to={`/roles/assign`}>Back to Assign Roles to users</Link>
      <br/>
      <Link to="/books/new">Back to adding new Books</Link>
     <br/>
      
      <Link to={`/books`}>Back to Books List</Link>
      <br/>
      {/* <Link to={`/publishers`}>Back to Publishers' list Page</Link> */}
     
      {/* <Link to={`/roles/create`}>Back to Creating new Roles Page</Link> */}
      <br/>
      {/* <Link to={`/roles/assign`}>Back to Assigning Roles to users Page</Link> */}
      <br/>
      {/* <Link to="/books/new">Back to Addong new Books</Link> */}
      
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