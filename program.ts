const config = require("./config.json");
const Discord = require('discord.js');
const fs = require("fs");
const program = new Discord.Client();
program.commands = new Discord.Collection();

fs.readdir("./cmds", (err, files) => {

  if(err) console.log(err);
  let jsfile = files.filter(f => f.split(".").pop() === "js");
  if(jsfile.length <= 0){
    console.log("Command not found.");
    return;
  }

  jsfile.forEach((f, i) =>{
    let props = require(`./cmds/${f}`);
    console.log(`${f} loaded!`);
    program.commands.set(props.program.name, props);
    console.log(props)
  });
});

});
program.on("message", async message => {

    if(message.channel.type === "dm") return;
  
    let {prefix} = config;
  
    if(!message.content.startsWith(prefix)) return;
  
    if(message.author.bot) return;  
    let messageArray = message.content.split(" ");
    let cmd = messageArray[0];
    let args = messageArray.slice(1);
  
    let commandfile = program.commands.get(cmd.slice(prefix.length));
    if(commandfile) commandfile.run(program,message,args)
    
  });
  
  program.login(config.token)