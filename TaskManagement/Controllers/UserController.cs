//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TaskManagement.Models;

//namespace TaskManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        static List<User> users = new List<User>();
//        static List<TaskItem> taskItems = new List<TaskItem>();


//        [HttpPost("CreateUser")]
//        public async Task<IActionResult> CreateUser(User _user)
//        {
//            users.Add(_user);
//            return Ok(_user);
//        }

//        [HttpGet("GetUser")]
//        public async Task<IActionResult> getUser(int id)
//        {
//            var userExists = users.FirstOrDefault(x => x.Id == id);
//            if (userExists == null)
//            {
//                return NotFound("User Does not exists");
//            }
//            else
//            {
//                return Ok(userExists);
//            }
//        }

//        [HttpPost("CreateTaskItem")]
//        public async Task<IActionResult> createTaskItem(TaskItem _taskItem)
//        {
//            var userId = users.FirstOrDefault(x => x.Id == _taskItem.assigneeId); 
//            if(userId == null)
//            {
//                return NotFound("No user found for assigneeId");
//            }
//            var assignedTask = taskItems.Where(x => x.assigneeId == _taskItem.assigneeId);
//            int count = 0;
//            foreach(var task in assignedTask)
//            {
//                if(task.status.ToLower() == "inprogress" || task.status.ToLower() == "todo")
//                {
//                    count += 1;
//                }
//            }
//            if(count >= 3)
//            {
//                return Ok("User already has 3 or more task in progress");
//            }
//            taskItems.Add(_taskItem);
//            return Ok(_taskItem);
//        }

//        [HttpGet("getUserTask")]
//        public async Task<IActionResult> getUserTask(int userId, string? status)
//        {
//            var user = users.FirstOrDefault(x => x.Id == userId);
//            if (user == null)
//            {
//                return NotFound("User Does not exists");
//            }
//            else
//            {
//                if (!string.IsNullOrEmpty(status))
//                {
//                    var userTask = taskItems.Where(x => x.assigneeId == userId && x.status.ToLower() == status.ToLower()).ToList();
//                    return Ok(userTask);
//                }
//                else
//                {
//                    return Ok(taskItems.Where(x => x.assigneeId == userId).ToList());
//                }
//            }
//        }

//        [HttpGet("changeTaskStatus")]
//        public async Task<IActionResult> changeTaskStatus(int userId, string status)
//        {
//            var user = users.FirstOrDefault(x => x.Id == userId);
//            if (user == null)
//            {
//                return NotFound("User Does not exists");
//            }
//            var userTask = taskItems.Find(x => x.assigneeId == userId);
//            userTask.status = status;
//            return Ok(userTask);
//        }

//        [HttpGet("overDueTask")]
//        public async Task<IActionResult> overDueTask()
//        {
//            var dueTask = taskItems.FindAll(x => x.dueDate < DateTime.Today && x.status.ToLower() != "done");
//            return Ok(dueTask);
//        }

//    }
//}
