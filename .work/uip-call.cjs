const fs = require('fs');
const {spawnSync} = require('child_process');
const cli = 'C:/Users/pewe/AppData/Roaming/npm/node_modules/@uipath/cli/dist/index.js';
const commands = JSON.parse(fs.readFileSync(process.argv[2], 'utf8'));
for (const args of commands) {
  const result = spawnSync(process.execPath, [cli, ...args], {stdio: 'inherit', windowsHide: true});
  if (result.error) throw result.error;
  if (result.status !== 0) process.exit(result.status || 1);
}
