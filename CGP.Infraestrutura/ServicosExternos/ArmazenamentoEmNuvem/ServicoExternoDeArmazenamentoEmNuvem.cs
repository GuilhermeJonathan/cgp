using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cgp.Infraestrutura.ServicosExternos.ArmazenamentoEmNuvem
{
    public class ServicoExternoDeArmazenamentoEmNuvem : IServicoExternoDeArmazenamentoEmNuvem
    {
        private readonly string _conta;
        private readonly string _chave;

        public ServicoExternoDeArmazenamentoEmNuvem(string conta, string chave)
        {
            if (string.IsNullOrEmpty(conta) || string.IsNullOrEmpty(chave))
                throw new InvalidOperationException("Não é possível criar um serviço de armazenamento em nuvem Azure sem a conta ou chave");

            this._conta = conta;
            this._chave = chave;
        }

        private void CriarContainersPadroes(IEnumerable<Tuple<string, bool>> containersPadroes)
        {
            var credenciais = new StorageCredentials(this._conta, this._chave);
            var storageAccount = new CloudStorageAccount(credenciais, true);

            var blobClient = storageAccount.CreateCloudBlobClient();

            foreach (var container in containersPadroes)
            {
                var BlobContainer = blobClient.GetContainerReference(container.Item1);

                if (container.Item2)
                    BlobContainer.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
                else
                    BlobContainer.CreateIfNotExists(BlobContainerPublicAccessType.Off);
            }
        }

        public async Task<string> EnviarArquivoAsync(Stream arquivo, string caminho, string nomeDoArquivo)
        {
            if (arquivo != null)
                arquivo.Position = 0;

            if (string.IsNullOrEmpty(caminho))
                throw new ArgumentException("É necessário informar no caminho pelo menos o nome do container");

            if (string.IsNullOrEmpty(nomeDoArquivo))
                throw new ArgumentException("Nome do arquivo não informado");


            var nomeDoContainer = caminho.Split('\\')[0];
            var diretorio = caminho;

            var credenciais = new StorageCredentials(this._conta, this._chave);
            var storageAccount = new CloudStorageAccount(credenciais, true);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(nomeDoContainer);

            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(!string.IsNullOrEmpty(diretorio) ? $"{nomeDoArquivo.Trim()}" :
                nomeDoArquivo);

            blockBlob.Properties.ContentType = MimeMapping.GetMimeMapping(nomeDoArquivo);

            try
            {
                await blockBlob.UploadFromStreamAsync(arquivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return blockBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<Stream> RecuperarArquivo(string caminho, string nomeDoArquivo)
        {
            if (string.IsNullOrEmpty(caminho))
                throw new ArgumentException("É necessário informar no caminho pelo menos o nome do container");

            if (string.IsNullOrEmpty(nomeDoArquivo))
                throw new ArgumentException("Nome do arquivo não informado");

            var caminhoDividido = caminho.Split('\\');

            var nomeDoContainer = caminho.Split('\\')[0];
            var diretorio = "";

            if (caminhoDividido.Length > 1)
                diretorio = caminho.Split('\\')[1];

            Stream streamDoArquivo = new MemoryStream();

            var credenciais = new StorageCredentials(this._conta, this._chave);
            var storageAccount = new CloudStorageAccount(credenciais, true);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(nomeDoContainer);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(!string.IsNullOrEmpty(diretorio) ? $"{diretorio}\\{nomeDoArquivo}" :
                nomeDoArquivo);

            if (!blockBlob.Exists())
                return streamDoArquivo;

            await blockBlob.DownloadToStreamAsync(streamDoArquivo);

            streamDoArquivo.Position = 0;

            return streamDoArquivo;
        }

    }
}
