using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.DataEntity;
using Joy.Storage;

namespace Portal.Winform
{
    public partial class TestLink : UserControl
    {
        public TestLink()
        {
            InitializeComponent();
            udg.OnGridLoad += udg_OnGridLoad;
        }

        private EntityGroup articles;
        private EntityGroup tags;

        private void udg_OnGridLoad()
        {
            articles = StorageManager.Instance.CreateOrGet("Articles");
            tags = StorageManager.Instance.CreateOrGet("Tags");
            var ts = new Tag[3];
            ts[0] = tags.AddEntity<Tag>(new Tag {Name = "news"});
            ts[1] = tags.AddEntity<Tag>(new Tag {Name = "adv"});
            ts[2] = tags.AddEntity<Tag>(new Tag {Name = "depth"});
            for (int i = 0; i < 10; i++)
            {
                Article a = articles.AddEntity<Article>(new Article {Caption = "cap_" + i, Content = "content_" + i});
                for (int j = 0; j <= i%3; j++)
                {
                    a.Tags.AddJoint(ts[j].ObjectKey);
                }
            }
            articles.Link();
            udg.Gv.DataSource = articles.GetEntities<Article>();
            udg.Gv.CellClick += GvDetails_CellClick;
        }

        void GvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Article a = articles.GetEntity<Article>(e.RowIndex);
            udg.GvDetails.DataSource = a.Tags.To<Tag>();
        }
    }
}
