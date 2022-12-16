const eventSource = new EventSource('http://localhost:5093/');
const dataArray = [];    
let paginationIndex = 0;

eventSource.addEventListener('message', (event) =>
{
  dataArray[event.lastEventId] = event.data; 

  if (event.lastEventId == 0) {
    document.querySelector('title').innerText = event.data;
  }

  present();
});

eventSource.addEventListener('close', eventSource.close);

document.addEventListener('keydown', (event) => {
  const keyCodes = {
    37: -1, // left arrow
    38: -1, // up arrow
    39: +1, // right arrow,
    40: +1 // down arrow
  };

  paginationIndex += (keyCodes[event.keyCode] || 0);
  paginationIndex = Math.max(paginationIndex, 0);
  paginationIndex = Math.min(paginationIndex, dataArray.length - 1);

  present();
});

function present() {
    let data = dataArray[paginationIndex];

    if (data.startsWith('@')) {
      data = `<img src="${data.substring(1)}" />`
    }

    if (data.startsWith('\\')) {
      data = data.substring(1);
    }

    document.getElementById('data').innerHTML = data;
    document.getElementById('counter').innerHTML = `${paginationIndex + 1} / ${dataArray.length}`;
}
