import { useState } from "react";
import { assignRole } from "../../services/authService";
import { Link } from "react-router-dom";



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
<Link to={`/publishers`}>Back to publishers Page</Link>
<br/>
      <Link to={`/roles/create`}>Back to CreateRole Page</Link>
      {/* <Link to={`/roles/assign`}>Back to Assign Role Page</Link> */}
      <br/>
      <Link to="/books/new">Back to Adding new Book</Link>
      <br/>
      <Link to="/publishers/new">Back to Adding Publishers page</Link>
      <br/>
      <Link to={`/books`}>Back to Book List</Link>
      <br/>
      {/* <Link to={`/publishers`}>Back to Publishers' list Page</Link> */}
     
      {/* <Link to={`/roles/create`}>Back to Creating new Roles Page</Link> */}
      <br/>
      {/* <Link to={`/roles/assign`}>Back to Assigning Roles to users Page</Link> */}
      {/* <br/> */}
      {/* <Link to="/books/new">Back to Addong new Books</Link> */}
      <br/>
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